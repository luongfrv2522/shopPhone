//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace shopPhone
{
    using System;
    using System.Collections.Generic;
    
    public partial class rela_brand_phone
    {
        public int id_brand { get; set; }
        public int id_phone { get; set; }
        public Nullable<int> position { get; set; }
    
        public virtual brand brand { get; set; }
        public virtual phone phone { get; set; }
    }
}
