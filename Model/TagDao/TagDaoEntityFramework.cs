using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, Int64>, ITagDao
    {
        /// <summary>Finds the name of the tag by.</summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns>The Tag</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Tag FindTagByName(string tagName)
        {
            Tag tag = null;

            DbSet<Tag> tags = Context.Set<Tag>();

            var result = (from t in tags where t.name == tagName select t);
            tag = result.FirstOrDefault();

            if (tag == null)
                throw new InstanceNotFoundException(tagName, typeof(Tag).FullName);

            return tag;
        }


        public List<Tag> TagByUses()
        {
            List<Tag> tagList = null;

            DbSet<Tag> tags = Context.Set<Tag>();

            var query = (from t in tags
                          orderby t.tagUses descending
                          select t);
            tagList = query.ToList();

            return tagList;
        }
    }
}