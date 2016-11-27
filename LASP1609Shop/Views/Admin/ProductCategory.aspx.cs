using LASP1609Shop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LASP1609Shop.Views.Admin
{
    public partial class ProductCategory : System.Web.UI.Page
    {
        LASP1609Entities _dataContext = new LASP1609Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                LoadData();
            }
        }

        private void LoadData()
        {
            if (Session["List"] == null)
            {
                 //Tra ra 1 danh sach ProductCategory
                var list = _dataContext.ProductCategories
                    .ToList();
                    //.OrderBy(x => x.Name);

                Session["List"] = list;
            }
           

            //Gan DataSource cho GridView
            grvListing.DataSource = Session["List"];
            grvListing.DataBind();

        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            pnlDetail.Visible = true;
           //Lấy giá trị Id của bản ghi
            var recordId = int.Parse(((LinkButton)sender).CommandArgument);
             //Đặt giá trị cho HiddenField
            hdRecordId.Value = recordId.ToString();

            var listRecords = _dataContext.ProductCategories;
            var record = listRecords.FirstOrDefault(x => x.Id == recordId);

            if (record != null)
            {
                txtName.Text = record.Name;
                txtDescription.Text = record.Description;
                chkActive.Checked = record.IsActive == null ? false : record.IsActive.Value;
            }
            //clear notify
            hdSuccessMessage.Value = string.Empty;
            hdErrorMessage.Value = string.Empty;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            pnlDetail.Visible = true;
            hdRecordId.Value = string.Empty;
        }

        private void ClearForm()
        {
            txtName.Text = string.Empty;
         
            txtDescription.Text = string.Empty;
            chkActive.Checked = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Lưu thông tin vào database
            var recordId = 0;
            var result = 0; //TODO check to show message - Buổi sau
            var check = int.TryParse(hdRecordId.Value, out recordId);
            if (check)
            {
                //Edit case
                var listRecords = _dataContext.ProductCategories;
                var record = listRecords.FirstOrDefault(x => x.Id == recordId);

                if (record != null)
                {
                    record.Name = txtName.Text;
                    record.Description = txtDescription.Text;
                    record.IsActive = chkActive.Checked;

                    result = _dataContext.SaveChanges();
                    if (result == 1)
                    {
                        hdSuccessMessage.Value = "Sửa bản ghi thành công";
                    }
                    else
                    {
                        hdErrorMessage.Value = "Có lỗi xảy ra";
                    }
                }
            }
            else
            {
                //Addnew case
                var record = new LASP1609Shop.DAL.ProductCategory
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    IsActive = chkActive.Checked
                };

                _dataContext.ProductCategories.Add(record);
                result = _dataContext.SaveChanges();
                if (result == 1)
                {
                    hdSuccessMessage.Value = "Tạo mới bản ghi thành công";
                }
                else
                {
                    hdErrorMessage.Value = "Có lỗi xảy ra";
                }

            }
            //Ẩn panel chi tiết
            pnlDetail.Visible = false;
            //Load data cho gridview
            LoadData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvListing.Rows.Count; i++)
            {
                CheckBox chkDelete = (CheckBox)grvListing.Rows[i].FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    var recordId = int.Parse(grvListing.DataKeys[i].Value.ToString());

                    var productCategories = _dataContext.ProductCategories;
                    var record = productCategories.FirstOrDefault(x => x.Id == recordId);
                    if (record != null)
                    {
                        _dataContext.ProductCategories.Remove(record);
                    }
                }
            }

            var result = _dataContext.SaveChanges();
            if (result == 1)
            {
                hdSuccessMessage.Value = "Xóa ghi thành công";
            }
            else
            {
                hdErrorMessage.Value = "Có lỗi xảy ra";
            }
            //Load lại data
            LoadData();
        }

        protected void grvListing_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvListing.PageIndex = e.NewPageIndex;

            //Load lai data
            LoadData();
        }

        protected void grvListing_Sorting(object sender, GridViewSortEventArgs e)
        {
            var dataSource = (List<LASP1609Shop.DAL.ProductCategory>)Session["List"];
            if (dataSource.Any())
            {
                var column = e.SortExpression;
                switch (column)
                {
                    case "Name":
                        dataSource = GetDirection() == SortDirection.Ascending
                            ? dataSource.OrderBy(x => x.Name).ToList()
                            : dataSource.OrderByDescending(x => x.Name).ToList();
                        break;
                    case "Description":
                        dataSource = GetDirection() == SortDirection.Ascending
                            ? dataSource.OrderBy(x => x.Description).ToList()
                            : dataSource.OrderByDescending(x => x.Description).ToList();
                        break;
                }
            }

            Session["List"] = dataSource;
            LoadData();
        }

        private SortDirection GetDirection()
        {
            if (ViewState["SortDirection"] == null || (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending)
            {
                ViewState["SortDirection"] = SortDirection.Descending;
            }
            else
            {
                ViewState["SortDirection"] = SortDirection.Ascending;
            }

            return (SortDirection) ViewState["SortDirection"];
        }
    }
}