<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="api1.aspx.cs" Inherits="GRCweb1.api1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
      var reqdata = {
         EmployeeName: "Robert Ben",
         empdetails: {
            email: 'robertben@hotmail.com',
            age: '50',
            salary: '10000',
            department: 'IT',
            totalexp: '10'
 
         }
      }
      var stringReqdata = JSON.stringify(reqdata);
 
      function GetEmpInfo() {
          //var url = "http://localhost:58475/api/Employee/GetEmpInfo?JSONString=" + stringReqdata;
          var url = "/API/JSONString=" + stringReqdata;
         jQuery.ajax({
            dataType: "json",
            url: url,
            async: false,
            context: document.body
         }).success(function (data) {
            alert(data);
         });
      };
      function SubmitEmpdata() {
         var url = "http://localhost:58475/api/Employee/PostSubmitdata";
         jQuery.ajax({
            async: false,
            type: "POST",
            url: url,
            data: stringReqdata,
            dataType: "json",
            context: document.body,
            contentType: 'application/json; charset=utf-8'
         }).success(function (data) {
            alert(data);
         })
      }
   </script>
        <a href=""></a>

       <a href="#" onclick="GetEmpInfo();">
      Get Employee</a><br />
   <a href="#" onclick="SubmitEmpdata();">
      Post Employee</a>
</asp:Content>
