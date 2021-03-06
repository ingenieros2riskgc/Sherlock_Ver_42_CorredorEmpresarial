using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOEstructuraCampo : clsDTOVariable
    {
        #region Properties
        private string strIdEstructCampo;
        private string strNombreCampo;
        private string strLongitud;
        private bool booEsParametrico;
        private string strNombreTipoDato;
        private string strIdTipoDato;
        private string strPosicion;

        public string StrIdEstructCampo
        {
            get { return strIdEstructCampo; }
            set { strIdEstructCampo = value; }
        }

        public string StrNombreCampo
        {
            get { return strNombreCampo; }
            set { strNombreCampo = value; }
        }

        public string StrLongitud
        {
            get { return strLongitud; }
            set { strLongitud = value; }
        }

        public bool BooEsParametrico
        {
            get { return booEsParametrico; }
            set { booEsParametrico = value; }
        }

        public string StrIdTipoDato
        {
            get { return strIdTipoDato; }
            set { strIdTipoDato = value; }
        }

        public string StrNombreTipoDato
        {
            get { return strNombreTipoDato; }
            set { strNombreTipoDato = value; }
        }

        public string StrPosicion
        {
            get { return strPosicion; }
            set { strPosicion = value; }
        }

        public int Estado { get; set; }
        #endregion Properties

        public clsDTOEstructuraCampo() { }

        public clsDTOEstructuraCampo(string strIdEstructCampo, string strNombreCampo, string strIdTipoDato, string strLongitud,
            bool booEsParametrico, string strIdVariable, string strNombreVariable, string strNombreTipoDato, string strPosicion, int estado)
        {
            this.StrIdVariable = strIdVariable;
            this.StrNombreVariable = strNombreVariable;
            this.StrIdEstructCampo = strIdEstructCampo;
            this.StrNombreCampo = strNombreCampo;
            this.StrLongitud = strLongitud;
            this.BooEsParametrico = booEsParametrico;
            this.StrIdTipoDato = strIdTipoDato;
            this.StrNombreTipoDato = strNombreTipoDato;
            this.StrPosicion = strPosicion;
            Estado = estado;
        }

        public string mtdConsultarPosicion()
        {
            return this.strPosicion;
        }
    }
}
