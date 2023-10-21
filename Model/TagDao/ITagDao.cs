using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, Int64>
    {
        /// <summary>Finds the name of the tag by.</summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns>The Tag</returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        Tag FindTagByName(String tagName);


        List<Tag> TagByUses();
    }
}