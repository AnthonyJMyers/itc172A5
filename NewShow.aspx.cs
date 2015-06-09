using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShowService;
public partial class NewShow : System.Web.UI.Page
{
    ShowService.NewShowServiceClient art = new NewShowServiceClient();
    private void FillArtistList()
    {
        Artist[] names;
        //List<Artist> names = new List<Artist>();
        names = art.GetArtists();
        ddlArtists.DataSource = names;
        ddlArtists.DataTextField = "ArtistName";
        ddlArtists.DataValueField = "ArtistKey";
        ddlArtists.DataBind();
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Id"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
        {
            FillArtistList();
        }
    }
    protected void ddlTitles_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //get the session value (the users id)
        int key = (int)Session["Id"];

        //create a new instance of the Review class
        //we get to it through the ReviewService object
        ShowService.Show rev = new ShowService.Show();
        //if the text box is not empty use the text in the text box
        if (!txtTitle.Text.Equals(""))
        {
            //create new book and author objects
            ShowService.Artist a = new ShowService.Artist();
            ShowService.Show b = new ShowService.Show();
            ShowService.ShowDetail d = new ShowService.ShowDetail();
            string c = null;
            //assign some values to the book
            a.ArtistName = txtTitle.Text;
            b.ShowName = txtShowName.Text;
            b.ShowDateEntered = DateTime.Now;

            //determine if the author text box has text
            if (!txtShowName.Text.Equals(""))
            {

                //if it does add the value to the author
                //and call the add author method in the service
                b.ShowName = txtShowName.Text;
                d.ArtistKey = int.Parse(ddlArtists.SelectedValue.ToString());
                d.ShowDetailArtistStartTime = TimeSpan.Parse("19:00:00");
                art.AddShow(b,d);

            }
            else
            {
                //otherwise get the author's name
                //from the drop down list
                a.ArtistName = ddlArtists.SelectedItem.Text.ToString();

            }

            //add the category
           // c = ddlCategory.SelectedItem.Text;

            //call the add Book method of the service
            art.AddShow(b, d);
            rev.ShowKey = b.ShowKey;

        }

        else
        {
            //Otherwise get the bookkey from the drop down list
            rev.ShowKey = int.Parse(ddlArtists.SelectedValue.ToString());

        }
        //add all the values to the review object
        rev.ShowDate = DateTime.Parse(txtDateTime.Text);
        rev.ShowKey = key;
        rev.ShowName = txtShowName.Text;
        rev.ShowTicketInfo = txtTicketInfo.Text;
        rev.VenueKey = int.Parse(txtVenue.Text);
        //rev. = txtReview.Text;
        //pass the review to the AddReview method
        ShowDetail shwdet = new ShowDetail();
        shwdet.ShowDetailAdditional = txtReview.Text;
        shwdet.ArtistKey = int.Parse(ddlArtists.SelectedValue);
        bool result = art.AddShow(rev, shwdet);
        //check the result to see if it wrote.
        if (result)
        {
            lbResult.Text = "Show Added";
        }
        else
        {
            lbResult.Text = "Show failed to save";
        }

    }
}