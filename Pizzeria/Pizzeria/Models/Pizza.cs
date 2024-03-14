namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pizza")]
    public partial class Pizza
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pizza()
        {
            Ordine = new HashSet<Ordine>();
        }

        [Key]
        public int IdPizza { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

       // [Required]
        public string Foto { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }

        public int TempoConsegna { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordine> Ordine { get; set; }
    }
}
