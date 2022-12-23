// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function openErrorModal(strMessage) {
    var myDiv = document.getElementById("MyModalErrorAlertBody");
    myDiv.innerHTML = strMessage;
    $('#myModalError').modal('show');
}

function openSuccessModal(strMessage) {
    var myDiv = document.getElementById("MyModalSuccessAlertBody");
    myDiv.innerHTML = strMessage;
    $('#myModalSuccess').modal('show');
}
function AddItem(btn) {
    var table;
    table = document.getElementById('CodesTable');
    var rows = table.getElementsByTagName('tr');
    var rowOuterHtml = rows[rows.length - 1].outerHTML;

    var lastrowIdx = rows.length - 2;

    var nextrowIdx = eval(lastrowIdx) + 1;

    rowOuterHtml = rowOuterHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOuterHtml = rowOuterHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOuterHtml = rowOuterHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);

    var newRow = table.insertRow();
    newRow.innerHTML = rowOuterHtml;

    var x = document.getElementsByTagName("INPUT");

    for (var cnt = 0; cnt < x.length; cnt++) {
        if (x[cnt].type == "text" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0) {
            if (x[cnt].id.indexOf('Unit') == 0)
                x[cnt].value = '';
        }
        else if (x[cnt].type == "number" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
            x[cnt].value = 0;
    }

    rebindvalidators();
}

function rebindvalidators() {
    var $form = $("#CodeSbyAnizForm");

    $form.unbind();

    $form.data("validator", null);

    $.validator.unobtrusive.parse($form);

    $form.validate($form.data("unobtrusiveValidation").options);
}

function DeleteItem(btn) {
    var table = document.getElementById('CodesTable');
    var rows = table.getElementsByTagName('tr');

    var btnIdx = btn.id.replaceAll('btnremove-', '');
    var idOfQuantity = btnIdx + "__Quantity";
    var txtQuantity = document.querySelector("[id$='" + idOfQuantity + "']");

    txtQuantity.value = 0;

    var idOfIsDeleted = btnIdx + "__IsDeleted";
    var txtIsDeleted = document.querySelector("[id$='" + idOfIsDeleted + "']");
    txtIsDeleted.value = "true";

    $(btn).closest('tr').hide();

    CalcTotals();
}
function getExRate(currencyid) {
    var lstbox = document.getElementById('dropdownExRate');

    var txtExrate = document.getElementById('txtExchangeRate');

    var items = lstbox.options;

    for (var i = items.length - 1; i >= 0; i--) {
        if (items[i].value == currencyid.value) {
            txtExrate.value = items[i].text;
            return;
        }
    }
    return;
}
function SetUnitName(txtProductCode) {
    var txtunitNameId = txtProductCode.id.replaceAll('ProductCode', 'UnitName');

    var txtunitName = document.getElementById(txtunitNameId);

    var lstbox = document.getElementById('dropdownUnitNames');

    var items = lstbox.options;

    for (var i = items.length - 1; i >= 0; i--) {
        if (items[i].value == txtProductCode.value) {
            txtunitName.value = items[i].text;
            return;
        }
    }
    return;
}
function setSameWidth(srcElement, desElement) {
    //style = window.getComputedStyle(srcElement);
    //wdt = style.getPropertyValue('width');
    //desElement.style.width = wdt;
    desElement.style.width = "230px";
}

function CalcTotals() {
    var x = document.getElementsByClassName('QtyTotal');

    var totalQty = 0;
    var Amount = 0;
    var totalAmount = 0;
    var txtExchangeRate = eval(document.getElementById('txtExchangeRate').value);

    var i;

    for (i = 0; i < x.length; i++) {
        var idofIsDeleted = i + "__IsDeleted";

        var idofPrice = i + "__PrcInBaseCur";

        var idofFob = i + "__Fob";

        var idofTotal = i + "__Total";

        var hidIsDelId = document.querySelector("[id$='" + idofIsDeleted + "']").id;

        var priceTxtId = document.querySelector("[id$='" + idofPrice + "']").id;

        var fobTxtId = document.querySelector("[id$='" + idofFob + "']").id;

        var totalTxtId = document.querySelector("[id$='" + idofTotal + "']").id;

        if (document.getElementById(hidIsDelId).value != "true") {
            totalQty = totalQty + eval(x[i].value);

            var txttotal = document.getElementById(totalTxtId);
            var txtprice = document.getElementById(priceTxtId);
            var txtfob = document.getElementById(fobTxtId);

            txtprice.value = txtExchangeRate * eval(txtfob.value);

            txttotal.value = eval(x[i].value) * txtprice.value;

            totalAmount = eval(totalAmount) + eval(txttotal.value);
        }
    }

    document.getElementById('txtQtyTotal').value = totalQty;
    document.getElementById('txtAmountTotal').value = totalAmount.toFixed(2);

    return;
}
document.addEventListener('change', function (e) {
    if (event.target.id.indexOf('ProductCode') >= 0) {
        SetUnitName(event.target);
    }
}, false);

document.addEventListener('change', function (e) {
    if (e.target.classList.contains('QtyTotal')
        || e.target.classList.contains('PriceTotal')
        || e.target.classList.contains('FobTotal')
        || event.target.id == 'PoCurrencyId'
    ) {
        CalcTotals();
    }
}
    , false);

function ShowSearchableList(event) {
    if (event.target.id.indexOf('ProductCode') < 0) {
        return;
    }

    var tid = event.target.id;

    var txtDescId = tid.replaceAll('ProductCode', 'Description');

    var txtValue = document.getElementById('txtValue');

    var txtText = document.getElementById(txtDescId);

    var txtSearch = event.target;

    var lstbox = document.getElementById("mySelect");

    $(txtSearch).after($(lstbox).show('slow'));

    if (event.keyCode === 13 || event.keyCode == 9) {
        txtSearch.value = txtValue.value;
        lstbox.style.visibility = "hidden";

        var divlst = document.getElementById("HiddenDiv");
        $(divlst).after($(lstbox).show('slow'));

        if (event.keyCode === 13) {
            event.preventDefault();
            txtSearch.focus();
            return;
        }
        else
            return;
    }

    setSameWidth(txtSearch, lstbox);
    lstbox.style.visibility = "visible";

    txtValue.value = "";
    txtText.value = "";

    var items = lstbox.options;
    for (var i = items.length - 1; i >= 0; i--) {
        if (items[i].text.toLowerCase().indexOf(txtSearch.value.toLowerCase()) > -1) {
            items[i].style.display = 'block';
            items[i].selected = true;
            txtValue.value = items[i].value;
            txtText.value = items[i].text;
        }
        else {
            items[i].style.display = 'none';
            items[i].selected = false;
        }
    }

    var objDiv = document.getElementById("CsDiv");
    objDiv.scrollTop = objDiv.scrollHeight - 200;
}
document.addEventListener('keydown', ShowSearchableList);