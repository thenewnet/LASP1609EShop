<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" AutoEventWireup="true" CodeBehind="ProductCategory.aspx.cs" Inherits="LASP1609Shop.Views.Admin.ProductCategory" %>

<%@ Register Src="~/UserControl/ctrSupport.ascx" TagPrefix="uc1" TagName="ctrSupport" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <link href="../../App_Themes/Admin/css/PagerStyle.css" rel="stylesheet" />
    <script src="../../App_Themes/Admin/js/product-category.js"></script>
    <%--Input for show notify purpose only--%>
    <input type="hidden" id="hdSuccessMessage" value="" runat="server"/>
    <input type="hidden" id="hdErrorMessage" value="" runat="server"/>

    <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">Danh sách Loại sản phẩm </h2>
            <uc1:ctrSupport runat="server" id="ctrSupport" />
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Danh sách                       
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">

                    <asp:GridView ID="grvListing" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Id" OnPageIndexChanging="grvListing_PageIndexChanging" PageSize="3" AllowSorting="True" OnSorting="grvListing_Sorting">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Tên" SortExpression="Name" />
                            <asp:BoundField DataField="Description" HeaderText="Miêu tả" HtmlEncode="false" SortExpression="Description"/>
                            <asp:BoundField DataField="IsActive" HeaderText="Hiển thị" />
                            <asp:TemplateField HeaderText="Chọn">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" OnClick="lbtnSelect_Click" 
                                        CommandArgument='<%# Eval("Id") %>'>Chi tiết</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Xóa">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDelete" runat="server" Style="font-weight: 700" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                    <asp:Button ID="btnAdd" runat="server" Text="Thêm mới" Width="100px" CssClass="btn-primary" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Xóa" Width="100px" OnClientClick="return confirm('Bạn có chắc muốn xóa không?')" CssClass="btn-danger" OnClick="btnDelete_Click" />
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <asp:Panel ID="pnlDetail" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Chi tiết                     
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-10">
                                <div class="form-group">
                                    <label>Tên</label>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Miêu tả</label>
                                    <%--<asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                    
                                    <CKEditor:CKEditorControl ID="txtDescription" CssClass="form-control" 
                                        runat="server" Height="400" Width="900" BasePath="~/ckeditor"></CKEditor:CKEditorControl>

                                </div>
                                <div class="form-group">
                                    <label>Hiển thị</label>
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="chkActive" runat="server" />
                                        </label>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdRecordId" runat="server" Value=""/>
                                <asp:Button ID="btnSave" runat="server" Text="Lưu thông tin" CssClass="btn-primary" Width="100px" OnClick="btnSave_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="Làm lại" CssClass="btn-primary" Width="100px" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
