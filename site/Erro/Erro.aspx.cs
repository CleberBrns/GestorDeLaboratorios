using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Erro_Erro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CarregaPagina();
    }

    private void CarregaPagina()
    {
        try
        {
            lblExcessao.Text = Session["ExcessaoDeErro"].ToString();

            if (!string.IsNullOrEmpty(lblExcessao.Text.Trim()))
            {
                lblComExcessao.Visible = true;
                btMenuPrincipal.Visible = true;
            }

        }
        catch (Exception ex) { }//Apenas exibe a página indicando o erro
    }

    protected void btMenuPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Home/Home.aspx");
    }

    protected void btSair_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Login/Login.aspx");
    }

}