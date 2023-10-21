using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService : ICommentService
    {
        [Inject]
        public IUserDao UserDao { set; private get; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ITagDao TagDao { set; private get; }

        [Inject]
        public ICommentDao CommentDao { set; private get; }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateCommentException"/>
        public long AddComment(long userId, long productId, string text, List<string> tags)
        {
            var user = UserDao.Find(userId);
            if (user == null)
            {
                //lanzar una nueva excepción NotLoginException ???
                throw new InstanceNotFoundException(userId, typeof(User).FullName);
            }

            var product = ProductDao.Find(productId);

            if (product == null)
            {
                throw new InstanceNotFoundException(productId, typeof(Product).FullName);
            }

            //si existe un comentario de ese usuario a ese producto --> excepcion
            bool exists = UserDao.ExistsCommentByUserIdAndProductId(userId, productId);
            if (exists)
                throw new DuplicateCommentException(userId, productId);

            //si no existe, permito la creación de uno nuevo
            Comment comment = new Comment();
            comment.userId = userId;
            comment.User = user;
            comment.productId = productId;
            comment.Product = product;
            comment.commentText = text;
            comment.createDate = DateTime.Now;
            List<Tag> lista = new List<Tag>();           
            foreach (string tagName in tags)
            {
                lista.Add(AddTagToComment(tagName));
            }
            
            comment.Tags = lista;
            CommentDao.Create(comment);

            return comment.commentId;
        }

        /// <summary>
        /// Adds the tag to comment.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Tag AddTagToComment(string tagName)
        {
            Tag tag = null;
            try
            {
                tag = TagDao.FindTagByName(tagName);
                tag.tagUses++;
                TagDao.Update(tag);
            }
            catch (InstanceNotFoundException)
            {
                tag = new Tag();
                tag.name = tagName;
                tag.tagUses = 1;

                TagDao.Create(tag);
            }
            return tag;
        }

        public long RemoveComment(long userId, long commentId)
        {
            //compruebo que el usuario existe
            var user = UserDao.Find(userId);
            if (user == null)
            {
                //lanzar una nueva excepción NotLoginException ???
                throw new InstanceNotFoundException(userId, typeof(User).FullName);
            }

            //compruebo que el comentario existe
            var comment = CommentDao.Find(commentId);
            if (comment == null)
            {
                throw new InstanceNotFoundException(commentId, typeof(Comment).FullName);
            }

            /* Borramos los usos de los tags en el comentario asociado */
            int aux = comment.Tags.Count;
            List<string> tagsList = new List<string>();

            for (int i=0; i < aux;i++)
            {
                Tag tags = comment.Tags.ElementAt(i);
                tagsList.Add(tags.name);
            }
            foreach(string tagName in tagsList)
            {
                DeleteTagFromComment(comment.commentId, tagName);
            }

            //compruebo que el comentario pertenece a ese usuario
            //si no pertence --> excepción
            bool isCorrect = UserDao.FindCommentByUserId(userId, commentId);
            if (!isCorrect)
                throw new NotPermittedRemoveCommentException(userId, commentId);

            //si pertenece --> elimino el comentario
            CommentDao.Remove(commentId);

            return commentId;
        }

        /// <summary>
        /// Deletes the tag from comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="tagName">Name of the tag.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        public void DeleteTagFromComment(long commentId, string tagName)
        {
            Comment comment = CommentDao.Find(commentId);

            if (comment == null)
                throw new InstanceNotFoundException(commentId, typeof(Comment).FullName);

            Tag tag = TagDao.FindTagByName(tagName);
            if (tag.tagUses == 0)
            {
                tag.tagUses = 0;
            } else
            {
                tag.tagUses--;
            }
           
            TagDao.Update(tag);

            comment.Tags.Remove(tag);

            CommentDao.Update(comment);
        }

        public Tag UpdateTagToComment(long commentId, string tagName)
        {
            var comment = CommentDao.Find(commentId);
            Tag tag = null;
            try
            {
                tag = TagDao.FindTagByName(tagName);
                var tagExists = comment.Tags.Contains(tag);

                if (tagExists == false)
                {
                    tag.tagUses++;
                    TagDao.Update(tag);
                }

            }
            catch (InstanceNotFoundException)
            {
                tag = new Tag();
                tag.name = tagName;
                tag.tagUses = 1;

                TagDao.Create(tag);
            }
            return tag;
        }

        public long UpdateComment(long userId, long commentId, string text, List<string> tags)
        {
            //compruebo que el usuario existe
            var user = UserDao.Find(userId);
            if (user == null)
            {
                //lanzar una nueva excepción NotLoginException ???
                throw new InstanceNotFoundException(userId, typeof(User).FullName);
            }

            //compruebo que el comentario existe
            var comment = CommentDao.Find(commentId);
            if (comment == null)
            {
                throw new InstanceNotFoundException(commentId, typeof(Comment).FullName);
            }

            //compruebo que el comentario pertenece a ese usuario
            //si no pertence --> excepción
            bool isCorrect = UserDao.FindCommentByUserId(userId, commentId);
            if (!isCorrect)
                throw new NotPermittedUpdateCommentException(userId, commentId);

            //si pertenece --> actualizo el comentario
            //actualizo el comentario
            Comment aux = CommentDao.Find(commentId);
            aux.commentText = text;

            List<Tag> listTag = new List<Tag>();
            foreach (string tagName in tags)
            {
                listTag.Add(UpdateTagToComment(commentId,tagName));
            }
            
            aux.Tags = listTag;
            CommentDao.Update(aux);
            return aux.commentId;
           
        }

        /// <exception cref="InstanceNotFoundException"/>
        public CommentBlock FindComentsByProductId(long productId, int startIndex, int size)
        {
            //compruebo que el producto existe
            var product = ProductDao.Find(productId);
            if (product == null)
                throw new InstanceNotFoundException(productId, typeof(Product).FullName);
            

            //en el caso de existir, obtengo sus comentarios
            List<Comment> comments = CommentDao.FindComentsByProductId(productId, startIndex, size + 1);
            List<CommentDetails> sol = new List<CommentDetails>();
            if (comments.Count != 0)
            {
                User user = null;
                foreach (Comment c in comments)
                {
                    user = UserDao.Find(c.userId);

                    /* Obtenemos los tags del comentario */
                    int aux = c.Tags.Count;
                    List<string> tagsList = new List<string>();

                    for (int i = 0; i < aux; i++)
                    {
                        Tag tags = c.Tags.ElementAt(i);
                        tagsList.Add(tags.name);
                    }

                    sol.Add(new CommentDetails(user.loginName, c.createDate, c.commentText, tagsList));
                }
            }
            bool existMoreItems = (sol.Count == size + 1);
            if (existMoreItems)
                sol.RemoveAt(size);

            return new CommentBlock(sol, existMoreItems);
        }


        /// <summary>
        /// Gets all tags created
        /// </summary>
        /// <returns>List with all tags created</returns>
        public List<string> GetAllTags()
        {
            var tags = TagDao.GetAllElements();

            return new List<string>(tags.Select(tag => tag.name));
        }

        public List<TagDetails> GetTagsByUse2()
        {
            var tagDetailsList = new List<TagDetails>();

            /* Asignamos a maxSize el el tagUses del primer elemento de la lista, el mayor,
             * si está vacía la lista asignamos 0 */
            int maxSize;
            var tagsList = TagDao.TagByUses();
            var aux = tagsList.Count;

            if (aux == 0)
            {
                maxSize = 0;
            } else
            {
                if (tagsList[0].tagUses == 0)
                {
                    maxSize = 0;
                }
                else
                {
                    maxSize = tagsList[0].tagUses;
                }

            }

            foreach (var item in tagsList)
            {
                //calculando el tamaño de fuente para cada etiqueta en la nube
                int fontSize = (int)Math.Round((double)item.tagUses / maxSize * 10) + 10;
                var tagDetails = new TagDetails()
                {
                    name = item.name,
                    uses = item.tagUses,
                    fontSize = fontSize
                };
                tagDetailsList.Add(tagDetails);
            }

            return tagDetailsList;
        }


        /// <summary> Gets the tags by uses </summary>
        /// <returns> A list of TagDetails ordered by the uses of the tags </returns>
        public List<TagDetails> GetTagsByUse()
        {
            var tags = TagDao.GetAllElements();
            var tagDetails = new List<TagDetails>();
            tags.ForEach(tag =>
            {
                tagDetails.Add(new TagDetails(tag.name, tag.tagUses));
            });

            tagDetails.Sort((tag1, tag2) => tag2.uses.CompareTo(tag1.uses));
            tagDetails = tagDetails.GetRange(0, Math.Min(10, tagDetails.Count));
            tagDetails.RemoveAll(x => x.uses == 0);

            if (tagDetails.Count == 0)
            {
                return tagDetails;
            }

            var maxUses = (float)tagDetails[0].uses;
            var minUses = (float)tagDetails[tagDetails.Count - 1].uses;

            for (var i = 0; i < (tagDetails.Count); i++)
            {
                //Este cálculo pendiente de corrección después de preguntarle al profe
                var aux = (tagDetails[i].uses - minUses) / (maxUses - minUses);

                tagDetails[i].fontSize = (int)Math.Round(10.0 * aux + 10.0);
            }

            return tagDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Comment GetComment(long userId, long productId)
        {
            Comment comment = CommentDao.FindComment(userId, productId);
            return comment;
        }

    }
}
