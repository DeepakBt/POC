<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Upload.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    
    <script>  

        function upload() {
            var myHeaders = new Headers();
            myHeaders.append("Content-Type", "application/json");
            myHeaders.append("Accept", "application/json");
            myHeaders.append("AppId", "41203F44M9E21A450CM9F97C5799B6471E43");
            myHeaders.append("", "");

            var formdata = new FormData();
            formdata.append("ReqExcelFile", fileInput.files[0], "/C:/Users/deepak/Downloads/AIFSIPDetailsUpload.xlsx");

            var requestOptions = {
                method: 'POST',
                headers: myHeaders,
                body: formdata,
                redirect: 'follow'
            };

            fetch("https://api.moamc.com/LMS/api/Sales/UploadAIFSIP", requestOptions)
                .then(response => response.text())
                .then(result => console.log(result))
                .catch(error => console.log('error', error));
        }



    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                  <input type="file" id="FileUpload2" />  
                    <input type="button" id="btnUpload" value="Upload Files" />  
        </div>
    </form>
</body>
</html>
