//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LASP1609Shop.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Nullable<int> ProductId { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
