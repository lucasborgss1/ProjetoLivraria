using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Editores
    {
        public decimal edi_id_editor { get; set; }
        public string edi_nm_editor { get; set; }
        public string edi_ds_email { get; set; }
        public string edi_ds_url { get; set; }

        public Editores(decimal adcIdEditor, string asNomeEditor, string asEmailEditor, string asUrlEditor)
        {
            this.edi_id_editor = adcIdEditor;
            this.edi_nm_editor = asNomeEditor;
            this.edi_ds_email = asEmailEditor;
            this.edi_ds_url = asUrlEditor;
        }
    }
}