//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Process
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Process()
        {
            this.Spectra = new HashSet<Spectrum>();
        }
    
        public long ID { get; set; }
        public string GeneralName { get; set; }
        public string ProcessId { get; set; }
        public byte[] Data { get; set; }
        public Nullable<long> Date { get; set; }
        public string OpticName { get; set; }
        public Nullable<long> LayerCount { get; set; }
        public Nullable<long> Duration { get; set; }
        public Nullable<long> EquipmentId { get; set; }
        public Nullable<long> QT { get; set; }
        public byte[] OpticDataCalc { get; set; }
        public byte[] OpticDataReal { get; set; }
        public Nullable<long> Test { get; set; }
    
        public virtual Equipment Equipment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Spectrum> Spectra { get; set; }
    }
}
