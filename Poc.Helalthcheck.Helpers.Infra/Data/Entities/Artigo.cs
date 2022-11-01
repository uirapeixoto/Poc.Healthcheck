using System;
using System.Collections.Generic;

namespace Poc.Healthcheck.Helpers.Infra.Data.Entities
{
    public partial class Artigo
    {
        public Artigo()
        {
            Comentarios = new HashSet<Comentario>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Corpo { get; set; } = null!;
        public string? Marcacao { get; set; }
        public int? IdCategoria { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
