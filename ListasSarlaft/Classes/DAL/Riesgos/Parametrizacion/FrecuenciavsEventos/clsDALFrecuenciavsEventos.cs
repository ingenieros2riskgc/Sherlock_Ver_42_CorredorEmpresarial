﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALFrecuenciavsEventos
    {
        public bool mtdInsertarFrecuenciavsEventos(clsDTOFrecuenciavsEventos objfrequencyEvents, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Parametrizacion].[FrecuenciavsEventos] ([EventosMaximos],[CodigoFrecuencia],[NombreFrecuencia],[UsuarioCreacion],[FechaRegistro])" +
                    "VALUES({0},{1},'{2}',{3},'{4}') ",
                    objfrequencyEvents.intEventosMaximos, objfrequencyEvents.intCodigoFrecuencia, objfrequencyEvents.strNombreFrecuencia, objfrequencyEvents.intIdUsuario, objfrequencyEvents.dtFechaRegistro);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la frecuencia de los eventos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdValidarInsertarFrecuenciavsEventos(clsDTOFrecuenciavsEventos objfrequencyEvents, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(1) as NumFE FROM[Parametrizacion].[FrecuenciavsEventos] where CodigoFrecuencia = {0}",
                    objfrequencyEvents.intCodigoFrecuencia);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                if (Convert.ToInt32(dtCaracOut.Rows[0]["NumFE"].ToString().Trim()) == 0)
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la frecuencia de los eventos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarFrecuenciavsEventos(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdFrecuenciaEventos],[EventosMaximos],[CodigoFrecuencia],[NombreFrecuencia],[UsuarioCreacion], Users.Usuario,[FechaRegistro]"
                    + " FROM [Parametrizacion].[FrecuenciavsEventos] as FE"
                    + " inner join Listas.Usuarios as Users on Users.IdUsuario = FE.UsuarioCreacion");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Frecuencias de los eventos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdateFrecuenciavsEventos(ref clsDTOFrecuenciavsEventos objFrequencyEvents, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Parametrizacion].[FrecuenciavsEventos] SET [EventosMaximos] = {0}, [CodigoFrecuencia] = {1}, [NombreFrecuencia] = '{2}'" +
                    " WHERE IdFrecuenciaEventos = {3} ",
                    objFrequencyEvents.intEventosMaximos, objFrequencyEvents.intCodigoFrecuencia, objFrequencyEvents.strNombreFrecuencia, objFrequencyEvents.intIdFrecuenciaEventos);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la frecuencia de los eventos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        //primero verifico si existe en la tabla de frecuencias [Riesgos].[ParamFrecEventos] 
        public bool mtdConsultarFrecuenciavsRiesgos(ref DataTable dtCaracOut, ref int idRiesgo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {              
                strConsulta = "\n"
                               + "SELECT fe.CodigoFrecuencia AS codFrecuencia , rr.idRiesgo, fe.NombreFrecuencia\n"
                               + "FROM riesgos.riesgo rr\n"
                               + "     INNER JOIN [Parametrizacion].[FrecuenciavsEventos] fe ON rr.idProbabilidad = fe.CodigoFrecuencia\n"
                               + "WHERE rr.idRiesgo = " + idRiesgo + ";";

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
                if (dtCaracOut.Rows.Count > 0)
                {
                    booResult = true;
                }
                else
                {
                    booResult = false;
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Frecuencias de los eventos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        //el riesgo ya tiene una frecuencia relacionada
        public bool mtdConsultarFrecuenciaRelacionada(ref DataTable dtCaracOut, ref int idRiesgo, ref string codFrecuencia, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {                 
                strConsulta = string.Format ("SELECT DISTINCT \n"
                           + "       rp.codFrecuencia AS IdFrecuenciaEventos, \n"
                           + "       rp.EventosMaximos      \n"
                           + "FROM [Riesgos].[ParamFrecEventos] rp    \n"
                           + "WHERE idRiesgo = (" + idRiesgo + ") AND rp.codFrecuencia	= (" + codFrecuencia + ");");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las Frecuencias de los eventos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

    }
}