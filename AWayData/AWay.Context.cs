﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AWayData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class AWayEntities : DbContext
    {
        public AWayEntities()
            : base("name=AWayEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Book> Books { get; set; }
        public DbSet<BookContent> BookContents { get; set; }
        public DbSet<EightLine> EightLines { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<vwPhotoData> vwPhotoDatas { get; set; }
    
        public virtual ObjectResult<spGetRandomPhoto_Result> spGetRandomPhoto()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetRandomPhoto_Result>("spGetRandomPhoto");
        }
    
        public virtual ObjectResult<Photo> getRandomPhoto()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Photo>("getRandomPhoto");
        }
    
        public virtual ObjectResult<Photo> getRandomPhoto(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Photo>("getRandomPhoto", mergeOption);
        }
    
        public virtual ObjectResult<vwPhotoData> spGetRandomPhotoData()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<vwPhotoData>("spGetRandomPhotoData");
        }
    
        public virtual ObjectResult<vwPhotoData> spGetRandomPhotoData(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<vwPhotoData>("spGetRandomPhotoData", mergeOption);
        }
    
        public virtual ObjectResult<getRandomBookText_Result> getRandomBookText()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getRandomBookText_Result>("getRandomBookText");
        }
    
        public virtual int getRandomBookTextFunctionImport()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getRandomBookTextFunctionImport");
        }
    }
}
