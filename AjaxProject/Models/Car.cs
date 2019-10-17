//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AjaxProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public int ModelId { get; set; }
        public int Year { get; set; }
        public decimal DailyPrice { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public bool Available { get; set; }
        public bool Status { get; set; }
    
        public virtual CarModel CarModel { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}