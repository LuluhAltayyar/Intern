using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace guide.intern
{
    public partial class intern : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  // show intern the effectivness of this code and how it trick them if they do not use it correctly
            {
                populateCountryCombo();
                populatecblHobby();
            }
        }
        protected void populateCountryCombo()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select countryId, country from country";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
                 ddlCountry.DataValueField = "countryId";
                ddlCountry.DataTextField = "country";
                ddlCountry.DataSource = dr;
                ddlCountry.DataBind();
        }
        protected void populatecblHobby()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select hobbyId, hobby from hobby";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
                cblHobbies.DataValueField = "hobbyId";
                cblHobbies.DataTextField = "hobby";
                cblHobbies.DataSource = dr;
                cblHobbies.DataBind();
           }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            int intInternId = 0;
            // I need to insert a new data to the database
            string mySql = "insert into intern (fNameEn,fNameAr,countryId,salary,active) " +
                "           values (@fNameEn,@fNameAr,@countryId,@salary,@active)" +
                                                     "SELECT CAST(scope_identity() AS int)";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@fNameEn", txtFnameEn.Text);
            myPara.Add("@fNameAr", txtFnameAr.Text);
            myPara.Add("@countryId", int.Parse(ddlCountry.SelectedValue.ToString()));
            myPara.Add("@salary", decimal.Parse(txtSalary.Text));
            int intActive = 0;
            if (rblActive.SelectedValue =="0")
            {
                intActive = 0;
            }
            else
            {
                intActive = 1;
            }
            myPara.Add("@active", intActive);
            CRUD myCrud = new CRUD();
            intInternId = myCrud.InsertUpdateDeleteViaSqlDicRtnIdentity(mySql, myPara);
            //   iterate cbl collection and capture the selected values
            List<int> mySelectedHobbies = new List<int>();
            foreach (ListItem item in cblHobbies.Items)
            {
                if (item.Selected)
                {
                    mySelectedHobbies.Add(int.Parse(item.Value));
                }
            }
            registerHobby(intInternId, mySelectedHobbies); // to read more about it ... This what we need to talk about
            showGvIntern(intInternId);
            showInternHobby(intInternId);
        }
        protected void registerHobby(int myInternId, List<int> mySelectedHobbies)
        {
            foreach (int item in mySelectedHobbies) // Loop through List with foreach
            {
                string mySql = @"INSERT INTO internHobby  (internId ,HobbyId)
                                       VALUES    (@internId,@HobbyId)";
                CRUD myCrud = new CRUD();
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@internId", myInternId);
                myPara.Add("@HobbyId", item);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            }
        }

        protected void fillTableWithHardCoded()
        {
            // I need to insert a new data to the database
            string mySql = "insert into intern (fNameEn,fNameAr) values ('Ali',N'علي')";   //
            CRUD myCrud = new CRUD();
            int rtn = myCrud.InsertUpdateDelete(mySql);
            if (rtn >= 1)
            { lblOutput.Text = "Success !"; }
            else
            { lblOutput.Text = "Failed !"; }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtFnameAr.Text = "";
            txtFnameEn.Text = "";
            txtInternId.Text = "";
            txtSalary.Text = "";
            ddlCountry.SelectedIndex = 0;
         
        }
        protected void btnUpdate_Click(object sender, EventArgs e)  
        {
            // I need to insert a new data to the database
            string mySql = @"Update intern set fNameEn =@fNameEn, fNameAr =@fNameAr, countryId =@countryId, salary =@salary ,active =@active
                                where internId = @internId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@internId", txtInternId.Text );
            myPara.Add("@fNameEn", txtFnameEn.Text);
            myPara.Add("@fNameAr", txtFnameAr.Text);
            myPara.Add("@countryId", int.Parse(ddlCountry.SelectedValue.ToString()));
            myPara.Add("@salary", decimal.Parse(txtSalary.Text));
            int intActive = 0;
            if (rblActive.SelectedValue == "0")
            {
                intActive = 0;
            }
            else
            {
                intActive = 1;
            }
            myPara.Add("@active", intActive);
            CRUD myCrud = new CRUD();
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            { lblOutput.Text = "Success !"; }
            else
            { lblOutput.Text = "Failed !"; }
            // call to populate the gv 
            showGvIntern();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // I need to insert a new data to the database
            string mySql = "delete intern where internId = @internId ";   //
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@internId", int.Parse(txtInternId.Text));  // demo for conversion 
            CRUD myCrud = new CRUD();
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            { lblOutput.Text = "Success !"; }
            else
            { lblOutput.Text = "Failed !"; }
            // call to populate the gv 
            showGvIntern();
        }
        protected void btnShowData_Click(object sender, EventArgs e)
        {
            showGvIntern();
            showInternHobby();
        }
        protected void showGvIntern()
        {
            string mySql = @"select  i.internId,fNameEn,fNameAr,i.countryId,country,salary,active 
                            from intern i inner join country c on i.countryId = c.countryId";
            CRUD myCrud = new CRUD();
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            gvIntern.DataSource = dr;
            gvIntern.DataBind();
        }
        protected void showGvIntern(int intInternId)
        {
            string mySql = @"select  i.internId,i.fNameEn,i.fNameAr,i.countryId,c.country,i.salary,i.active 
                            from intern i inner join country c on i.countryId = c.countryId
                                where i.internId = @internId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@internId", intInternId);  // demo for conversion 
            CRUD myCrud = new CRUD();
            SqlDataReader dr = myCrud.getDrPassSql(mySql,myPara);
            gvIntern.DataSource = dr;
            gvIntern.DataBind();
        }
        protected void btnShowIntern_Click(object sender, EventArgs e)
        {
            if (txtInternId.Text == "")
            {
                lblOutput.Text = " Please enter intern Id !";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                return;
            }
            int myInternId = int.Parse( txtInternId.Text);
            showGvIntern(myInternId);
        }
        protected void showInternHobby()
        {
            string mySql = @"  select i.internId,i.fnameen ,ih.hobbyId,ih.hobby
                   from intern i inner join internHobby ih on i.internid = ih.internId inner join hobby h on ih.hobbyId = h.hobbyId ";
            CRUD myCrud = new CRUD();
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            gvInternHobby.DataSource = dr;
            gvInternHobby.DataBind();
        }
        protected void showInternHobby( int intInternId)
        {
            string mySql = @"  select i.internId,i.fnameen ,ih.hobbyId,ih.hobby
                   from intern i inner join internHobby ih on i.internid = ih.internId inner join hobby h on ih.hobbyId = h.hobbyId
                        where i.internId = @InternId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@InternId", intInternId);
            CRUD myCrud = new CRUD();
            SqlDataReader dr = myCrud.getDrPassSql(mySql,myPara);
            gvInternHobby.DataSource = dr;
            gvInternHobby.DataBind();
        }
    }//class 
} //NS