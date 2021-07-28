//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestaoEconomato
{
    using System;
    using System.Collections.Generic;
    
    public partial class EncomendasCliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EncomendasCliente()
        {
            this.ProdutoEncomendarClientes = new HashSet<ProdutoEncomendarCliente>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> data_entregue { get; set; }
        public System.DateTime data_pedido { get; set; }
        public string Id_Cliente { get; set; }
        public string estado { get; set; }
        public string Id_Funcionario { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdutoEncomendarCliente> ProdutoEncomendarClientes { get; set; }
    }
}
