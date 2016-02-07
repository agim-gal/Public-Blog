using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Public_Blog.Models
{
    public static class ContentRepository
    {
        //Метод достаёт изображение из базы по id
        public static Image getImage(int id)
        {
            Image image;
            using (DBContext db = new DBContext())
            {
                image = db.Images.FirstOrDefault(img => img.Id_image == id);
            }
            return image;        
        }

        //Метод достаёт пост из базы по id
        public static PostView getPost(int id)
        {
            Post post;
            using (DBContext db = new DBContext())
            {
                post = db.Posts.FirstOrDefault(p => p.Id_post == id);
                return postToPostView(post);
            }           
        }

        //Возвращает список постов по указанному тегу для указанной страницы
        public static List<PostView> getPostsByTag(string tag, int page)
        {
            IQueryable<Post> posts = getPosts().tag(tag).onPage(page);
            List<PostView> postViewList = postListToPostViewList(posts);
            return postViewList;
        }

        //Возвращает количество страниц на которых можно разместить все посты по тегу
        public static int getCountPageByTag(string tag)
        {
            IQueryable<Post> posts = getPosts().tag(tag);
            return posts.getCountPage();
        }

        //Возвращает список постов для указанного пользователя для указанной страницы
        public static List<PostView> getPostsByUser(string username, int page)
        {
            IQueryable<Post> posts = getPosts().user(username).onPage(page);
            List<PostView> postViewList = postListToPostViewList(posts);
            return postViewList;
        }
        //Возвращает количество страниц на которых можно разместить все посты юзера
        public static int getCountPageByUser(string username)
        {
            IQueryable<Post> posts = getPosts().user(username);
            return posts.getCountPage();
        }

        //Возвращает список постов для указанной страницы
        public static List<PostView> getPosts(int page)
        {
            IQueryable<Post> posts = getPosts().onPage(page);
            List<PostView> postViewList = postListToPostViewList(posts);
            return postViewList;
        }

        //Возвращает количество страниц на которых можно разместить все посты
        public static int getCountPage()
        {
            IQueryable<Post> posts = getPosts();
            return posts.getCountPage();
        }

        //Материализует Post и преобразывает его во PostView
        static PostView postToPostView(Post post)
        {
            PostView postView = new PostView();
            if (post != null)
            {
                postView.Header = post.Header ?? " ";
                if (post.Image!=null)
                {
                    postView.id_image = post.Image.Id_image;
                }
                postView.id_post = post.Id_post;
                postView.tags = post.Tags.Select(tag => tag.name).ToList();
                postView.textContent = post.Text ?? "";
                postView.username = post.User.Login ?? "";

                return postView;
            }
            else
            {
                throw new PostNotExistException();
            }
        }

        //преобразовывает список Post в список PostView по правилу postToPostView
        static List<PostView> postListToPostViewList(IEnumerable<Post> postList)
        {
            return postList.Select(postToPostView).ToList();
        }

        //Возвращает количество страниц на которых можно разместить данный список постов
        static int getCountPage(this IQueryable<Post> posts)
        {
            return (int)Math.Ceiling(posts.Count() / (double)Configs.countPostsOnPage());
        }

        //добавляем новый пост в базу
        //postModel - текстовые данные введённый пользователем
        //image - изображение в том виде, в каком мы получили его от браузера
        //username - имя пользователя, которому принадлежит пост
        public static void addPost(CreatePostModel postModel, HttpPostedFileBase image, string username)
        {
            using (DBContext db = new DBContext())
            {
                Post post = new Post();

                //добавление к записи заголовка и текста
                post.Header = postModel.Header;
                post.Text = postModel.Text;

                //добавляем изображение
                if (image != null)
                {
                    post.Image = new Image
                    {
                        ImageMimeType = image.ContentType,
                        ImageData = new byte[image.ContentLength]
                    };
                    //записываем битарей изображения
                    image.InputStream.Read(post.Image.ImageData, 0, image.ContentLength);
                }

                //добавляем пользователя, автора поста
                post.User = db.Users.FirstOrDefault(u => u.Login == username);
                Debug.Assert(post.User != null, "пользователь не существует");
                
                //сохранение в базе
                db.Posts.Add(post);
                db.SaveChanges();

                //добавляем теги в базу
                var tags = addTags(postModel.Tags);
                
                //ассоциируем с ними пост
                relatedTagsAndPost(post.Id_post, tags);
            }
        }

        //Метод добавляет теги в базу 
        private static ICollection<Tag> addTags(string tagsString)
        {
            using (var db = new DBContext())
            {

                //разбиваем строку тегов на список строк по ','
                //откидываем лишние пробелы по краям тегов
                IEnumerable<string> listTagString = tagsString.Split(',').Select(s => s.Trim());

                //создаём список тегов из listTagString
                //каждая строка это имя тега
                ICollection<Tag> tags = listTagString.Select(tag => new Tag()
                {
                    name = tag
                }).ToList();

                //добавляем теги в базу
                //если тег там уже есть, то не пытаемся добавить его снова
                foreach (Tag tag in tags)
                {
                    if (!db.Tags.Any(t => t.name == tag.name))
                    {
                        db.Tags.Add(tag);
                    }
                }

                //сохраняем изменения
                db.SaveChanges();
                return tags;
            }
        }

        

        //Метод ассоциирует пост с тегами
        private static void relatedTagsAndPost(int postId, ICollection<Tag> tagList)
        {

            using (var db = new DBContext())
            {
                //берём из базы пост с указанным id
                Post post = db.Posts.FirstOrDefault(p => p.Id_post == postId);

                //ищем в базе наши теги и оссоциируем с ними данный пост
                foreach (var element in tagList)
                {
                    Tag tag = db.Tags.FirstOrDefault(t => t.name == element.name);
                    post.Tags.Add(tag);
                }

                //сохраняем изменения
                db.SaveChanges();
            }
        }
        
        //удаление поста и связанных с ним данных
        public static void removePost(int postId, string username)
        {
            
            using (var db = new DBContext())
            {
                //ищим пост предназначенный для удаления
                //за одно проверяем, принадлежит ли он данному юзеру
                Post post = db.Posts.FirstOrDefault(p => p.Id_post == postId && p.User.Login == username);
                //если пост есть и у юзера есть права на его удаление
                if (post != null)
                {
                    //ищем и удаляем изображение из поста
                    Image image = post.Image;
                    if (image != null) db.Images.Remove(image);

                    //ищем и удаляем теги, которые больше не используются
                    //это такие теги, с которыми связан только 1 пост
                    var unuseTags = post.Tags.Where(p => p.Posts.Count == 1);
                    db.Tags.RemoveRange(unuseTags);

                    //удаляем сам пост и сохраняем изменения
                    db.Posts.Remove(post);
                    db.SaveChanges();
                }
                else
                {
                    throw new PostNotExistOrNotAccessException();
                }
            }
        }

        //Возвращает запрос для получения всез постов
        static IQueryable<Post> getPosts()
        {
            return (new DBContext()).Posts.OrderByDescending(p => p.Id_post);
        }

        //Возвращает запрос для фильтрации постов по юзеру
        static IQueryable<Post> user(this IQueryable<Post> posts, string username)
        {
            return posts.Where(p => p.User.Login == username);
        }

        //Возвращает запрос для фильтрации постов по тегу
        static IQueryable<Post> tag(this IQueryable<Post> posts, string tagname)
        {
            return posts.Where(p => p.Tags.Where(t => t.name == tagname).Any());
        }

        //Возвращает запрос для получения постов с определённой страницы,
        //то есть пропускает все стоящие впереди посты и 
        //возвращает определённое в классе конфигрурации количество постов
        static IQueryable<Post> onPage(this IQueryable<Post> posts, int page)
        {
            int countPostsOnPage = Configs.countPostsOnPage();
            return posts.Skip((page - 1) * countPostsOnPage).Take(countPostsOnPage);
        }

        
    }

    #region exceptions
    [Serializable]
    public class PostNotExistException : Exception
    {
        public PostNotExistException() { }
        public PostNotExistException(string message) : base(message) { }
        public PostNotExistException(string message, Exception inner) : base(message, inner) { }
        protected PostNotExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class PostNotExistOrNotAccessException : Exception
    {
        public PostNotExistOrNotAccessException() { }
        public PostNotExistOrNotAccessException(string message) : base(message) { }
        public PostNotExistOrNotAccessException(string message, Exception inner) : base(message, inner) { }
        protected PostNotExistOrNotAccessException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }


    #endregion
}