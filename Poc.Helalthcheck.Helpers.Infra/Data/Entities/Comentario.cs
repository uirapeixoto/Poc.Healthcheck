using System;
using System.Collections.Generic;

namespace Poc.Healthcheck.Helpers.Infra.Data.Entities
{
    public partial class Comentario
    {
        public Comentario()
        {
            InverseIdComentarioNavigation = new HashSet<Comentario>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public int? IdComentario { get; set; }
        public int IdUsuario { get; set; }
        public int IdArtigo { get; set; }
        public bool? Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }

        public virtual Artigo IdArtigoNavigation { get; set; } = null!;
        public virtual Comentario? IdComentarioNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Comentario> InverseIdComentarioNavigation { get; set; }
    }
}
