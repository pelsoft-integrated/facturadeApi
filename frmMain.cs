using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Fwk.Logging.Targets;
using Fwk.HelperFunctions;
using CELAM.BE;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using CELAM.Front.Socios;
using CELAM.Front.Facturas;
using CELAM.Front.Common;
using pelfost.licence;

namespace CELAM.Front
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
       
        UserControl currentControl;
        uc_Factura uc_Factura = new uc_Factura();
        uc_ExportFacturas uc_ExportFacturas1 = new uc_ExportFacturas();
        uc_Socios_Importacion_v2 uc_Socios_Imp_2 = new uc_Socios_Importacion_v2();
        uc_Socios_Importacion uc_Socios_Check_Imp = new uc_Socios_Importacion();
        uc_Socios_Autorizados uc_Socios_Aut = new uc_Socios_Autorizados();
        uc_Socios_Asignar uc_Socios_Asig = new uc_Socios_Asignar();
        uc_Socios_SolicitudRegistro uc_Socios_SolicitudRegistro  = new  uc_Socios_SolicitudRegistro ();
        uc_news uc_news = new uc_news();
        uc_web_users uc_web_users = new uc_web_users();
        uc_news_list uc_news_list = new uc_news_list();
        uc_Settings uc_Settings = new uc_Settings();
        us_SociosCounts us_SociosCounts = new us_SociosCounts();
        public frmMain()
        {
            InitializeComponent();
            try
            {
             
                this.Text = string.Concat(this.Text, " version ", Assembly.GetExecutingAssembly().GetName().Version.ToString());


                this.Text = this.Text + " SourceInfo = " + Fwk.Bases.WrapperFactory.GetWrapper("").SourceInfo;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Boolean registerOk = false;
            try
            {
                lic.chk();
                registerOk = true;
            }
            catch (Exception ex)
            {

                using (frmRegisterHealth frm = new frmRegisterHealth())
                {
                    frm.Text = ex.Message;
                    frm.ShowDialog();
                    registerOk = frm.RegisterOk;
                }
            }

            navBarControl1.Enabled = registerOk;
        }






        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                

                Boolean registerOk = false;
                try
                {
                    lic.chk();
                    registerOk = true;
                }
                catch (Exception ex)
                {

                    using (frmRegisterHealth frm = new frmRegisterHealth())
                    {
                        frm.Text = ex.Message;
                        frm.ShowDialog();
                        registerOk = frm.RegisterOk;
                    }
                }
                if (String.IsNullOrEmpty(AppSettings.store.StorageObject.NewsWebSitePath))
                    AddContronToPannel(uc_Settings);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmMain_Leave(object sender, EventArgs e)
        {
            AppSettings.store.Save();

        }
        
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            AppSettings.store.Save(); uc_ExportFacturas1.Abort();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddContronToPannel(uc_Socios_Imp_2);
        }

        void AddContronToPannel(UserControl ctrl)
        {

            if (!this.splitContainer1.Panel2.Contains(ctrl))
            {
                currentControl = ctrl;

                this.splitContainer1.Panel2.Controls.Add(ctrl);
                ctrl.Location = new System.Drawing.Point(0, 0);
                ctrl.Dock = System.Windows.Forms.DockStyle.Fill;


            }
            ctrl.Refresh();
            ctrl.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddContronToPannel(uc_Socios_Aut);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            AddContronToPannel(uc_Socios_Asig);
        }




        private void button4_Click_1(object sender, EventArgs e)
        {
            AddContronToPannel(uc_news_list);
        }

        private void nvSettings_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_Settings);
        }

        private void nvImportarFacturas_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_ExportFacturas1);
        }

        private void nvImportarUsuarios_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_Socios_Imp_2);
        }

        private void nvAsignUsuarioWeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_Socios_Asig);
        }



        private void navSociosAut_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_Socios_Aut);

        }


        private void nvConsultar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_news_list);
        }

        private void nvNews_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_news);
        }

        private void navEditWebUser_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_web_users);
        }
        CELAM.Front.Statistics.uc_Statistic_1 uc_Statistic_1 = new Statistics.uc_Statistic_1();

        private void nvSetatistics_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_Statistic_1);
        }
       
        private void navBar_SolicitudesRegistro_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_Socios_SolicitudRegistro);

        }

      
        private void navBarSociosCount_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(us_SociosCounts);
        }
        us_FacturasCounts us_FacturasCounts = new us_FacturasCounts();
        private void nvFacturasCount_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(us_FacturasCounts);
        }
        uc_Facturas_Gestionar uc_Facturas_Gestionar = new uc_Facturas_Gestionar ();
        private void nvGestionar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_Facturas_Gestionar);
        }

        private void nvChequeoPrevioImportar_ItemChanged(object sender, EventArgs e)
        {
           
            
        }

        private void nvChequeoPrevioImportar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_Socios_Check_Imp);

        }
        uc_TomaEstados uc_TomaEstados = new uc_TomaEstados();
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
           
        }

        private void iImportTomaEstado_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_TomaEstados);
        }
        uc_TomaEstadoAnalisis uc_TomaEstadoAnalisis = new uc_TomaEstadoAnalisis();
        private void iAnalizeTomaEstados_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_TomaEstadoAnalisis);
        }
        uc_TomaEstadoTotales uc_TomaEstadoTotales = new uc_TomaEstadoTotales();
        private void navBarItem1_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_TomaEstadoTotales);
        }
        uc_TomaEstadoCubo uc_TomaEstadoCubo = new  uc_TomaEstadoCubo();
        private void iTomaestadoCubo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_TomaEstadoCubo);
        }

        private void nvViewFactura_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_Factura);
        }
        uc_FacturacionDisponibleEnvio uc_FacturacionDisponibleEnvio = new uc_FacturacionDisponibleEnvio(); 
        private void iEnvioMasivoFacturacionDisponible_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddContronToPannel(uc_FacturacionDisponibleEnvio);

        }
    }
}
