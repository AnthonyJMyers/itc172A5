using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //instantiate service
        LoginRegistrationService.VenueLoginRegistrationServiceClient rc = new LoginRegistrationService.VenueLoginRegistrationServiceClient();
      

        //create an instance of the venue class from our data Contract
        LoginRegistrationService.Venue ven = new LoginRegistrationService.Venue();
        LoginRegistrationService.VenueLogin venlog = new LoginRegistrationService.VenueLogin();

        //assign text box content to the venue properties
        ven.VenueName = txtVenueName.Text;
        ven.VenueEmail = txtVenueEmail.Text;
        ven.VenueAddress = txtVenueAddress.Text;
        ven.VenueAgeRestriction = int.Parse(txtVenueAgeRestriction.Text);
        ven.VenueCity = txtVenueCity.Text;
        ven.VenueDateAdded = DateTime.Now;
        ven.VenuePhone = txtVenuePhone.Text;
        ven.VenueState = txtVenueState.Text;
        ven.VenueWebPage = txtVenueWebPage.Text;
        ven.VenueZipCode = txtVenueZipCode.Text;

        venlog.VenueLoginUserName = txtUserName.Text;
        venlog.VenueLoginPasswordPlain = txtConfirm.Text;

        //call the register method in the service
        bool result = rc.RegisterVenue(ven, venlog);
        //check the results
        if (result)
        {
            lblResult.Text = "You have successfully registered";
        }
        else
        {
            lblResult.Text = "Registration failed";
        }


    }
}