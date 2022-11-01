using System;
using System.Collections.Generic;

namespace Poc.Healthcheck.Helpers.Infra.Data.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            Artigos = new HashSet<Artigo>();
            Comentarios = new HashSet<Comentario>();
        }

        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Sexo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }

        public virtual ICollection<Artigo> Artigos { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
