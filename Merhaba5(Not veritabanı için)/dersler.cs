//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Merhaba5_Not_veritabanı_için_
{
    using System;
    using System.Collections.Generic;
    
    public partial class dersler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dersler()
        {
            this.ogrenci_not = new HashSet<ogrenci_not>();
        }
    
        public int DERSID { get; set; }
        public string DERSAD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ogrenci_not> ogrenci_not { get; set; }
    }
}
