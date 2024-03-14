namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordine")]
    public partial class Ordine
    {
        [Key]
        public int IdOrdine { get; set; }

        public int FK_IdPizza { get; set; }

        public int? FK_IdBibita { get; set; }

        public int FK_IdUtente { get; set; }

        [Required]
        public string IndirizzoConsegna { get; set; }

        public int Quantita { get; set; }

        public string Nota { get; set; }

        public decimal Totale { get; set; }

        public virtual Bibita Bibita { get; set; }

        public virtual Pizza Pizza { get; set; }

        public virtual Users Users { get; set; }
    }
}
