//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TraderWPF
{
    using System;
    using System.Collections.Generic;
    
    public partial class acheter
    {
        public int numAction { get; set; }
        public int numTrader { get; set; }
        public double prixAchat { get; set; }
        public int quantite { get; set; }
    
        public virtual action action { get; set; }
        public virtual trader trader { get; set; }
    }
}