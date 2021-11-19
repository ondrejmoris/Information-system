<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Vytvoření objednávky:</h1>
    </div>
    <div class="row">
        <div class="col-sm-6">
            <div class="form" id="order" runat="server">
                <label for="optradio">Typ počítače:</label>
              <div class="form-check">    
                <label class="form-check-label">
                    <input type="radio" class="form-check-input" id="radN" value="1" name="optradio" runat="server"> Notebook
                </label>
               </div>
                <div class="form-check">
                <label class="form-check-label">
                    <input type="radio" class="form-check-input" id="radS" value="2" name="optradio" runat="server"> Stolní pc
                </label>
               </div>
               <div class="form-group">
                <label for="Desc">Popis problému:</label>
                <textarea class="form-control" rows="5" id="Desc" runat="server"></textarea>
               </div> 
              <asp:Button
                ID="confirm"
                text="Potvrdit"
                onclick="confirm_Click"
                runat="server"
               />
            </div>
           </div>
        <div class="col-sm-6">
            <div class="form" id="personal" runat="server">
                <div class="form-group">
                 <label for="Phone">Tel.:</label>
                  <input type="number" id="Phone" min="0" runat="server">
                 </div>
                <div class="form-group">
                 <label for="city">Město:</label>
                    <input type="text" id="city" name="city" runat="server"> 
                    </div>
                <div class="form-group">
                <label for="address">Adresa:</label>
                    <input type="text" id="address" name="address" runat="server">
                    </div>
                <div class="form-group">
                <label for="houseNum">Číslo domu:</label>
                  <input type="number" id="houseNum" name="houseNum" min="0" runat="server"> 
                    </div>
              <asp:Button
                ID="confirmPers"
                text="Potvrdit"
                onclick="confirmPers_Click"
                runat="server"
               />
            </div>
        </div> 
    </div>
    

    

</asp:Content>
