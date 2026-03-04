var SuccessMessage = "ok";

/*!
 * dragtable
 *
 * @Version 2.0.15
 *
 * Copyright (c) 2010-2013, Andres akottr@gmail.com
 * Dual licensed under the MIT (MIT-LICENSE.txt)
 * and GPL (GPL-LICENSE.txt) licenses.
 *
 * Inspired by the the dragtable from Dan Vanderkam (danvk.org/dragtable/)
 * Thanks to the jquery and jqueryui comitters
 *
 * Any comment, bug report, feature-request is welcome
 * Feel free to contact me.
 */
(function ($) {
    $.widget("akottr.dragtable", {
        options: {
            revert: false,               // smooth revert
            dragHandle: '.table-handle', // handle for moving cols, if not exists the whole 'th' is the handle
            maxMovingRows: 40,           // 1 -> only header. 40 row should be enough, the rest is usually not in the viewport
            excludeFooter: false,        // excludes the footer row(s) while moving other columns. Make sense if there is a footer with a colspan. */
            onlyHeaderThreshold: 100,    // TODO:  not implemented yet, switch automatically between entire col moving / only header moving
            dragaccept: null,            // draggable cols -> default all
            persistState: null,          // url or function -> plug in your custom persistState function right here. function call is persistState(originalTable)
            restoreState: null,          // JSON-Object or function:  some kind of experimental aka Quick-Hack TODO: do it better
            exact: true,                 // removes pixels, so that the overlay table width fits exactly the original table width
            clickDelay: 100,              // ms to wait before rendering sortable list and delegating click event
            containment: null,           // @see http://api.jqueryui.com/sortable/#option-containment, use it if you want to move in 2 dimesnions (together with axis: null)
            cursor: 'move',              // @see http://api.jqueryui.com/sortable/#option-cursor
            cursorAt: false,             // @see http://api.jqueryui.com/sortable/#option-cursorAt
            distance: 0,                 // @see http://api.jqueryui.com/sortable/#option-distance, for immediate feedback use "0"
            tolerance: 'pointer',        // @see http://api.jqueryui.com/sortable/#option-tolerance
            axis: 'x',                   // @see http://api.jqueryui.com/sortable/#option-axis, Only vertical moving is allowed. Use 'x' or null. Use this in conjunction with the 'containment' setting
            beforeStart: $.noop,         // returning FALSE will stop the execution chain.
            beforeMoving: $.noop,
            beforeReorganize: $.noop,
            beforeStop: $.noop
        },
        originalTable: {
            el: null,
            selectedHandle: null,
            sortOrder: null,
            startIndex: 0,
            endIndex: 0
        },
        sortableTable: {
            el: $(),
            selectedHandle: $(),
            movingRow: $()
        },
        persistState: function () {
            var _this = this;
            this.originalTable.el.find('th').each(function (i) {
                if (this.id !== '') {
                    _this.originalTable.sortOrder[this.id] = i;
                }
            });
            $.ajax({
                url: this.options.persistState,
                data: this.originalTable.sortOrder
            });
        },
        /*
         * persistObj looks like
         * {'id1':'2','id3':'3','id2':'1'}
         * table looks like
         * |   id2  |   id1   |   id3   |
         */
        _restoreState: function (persistObj) {
            for (var n in persistObj) {
                this.originalTable.startIndex = $('#' + n).closest('th').prevAll().length + 1;
                this.originalTable.endIndex = parseInt(persistObj[n], 10) + 1;
                this._bubbleCols();
            }
        },
        // bubble the moved col left or right
        _bubbleCols: function () {
            var i, j, col1, col2;
            var from = this.originalTable.startIndex;
            var to = this.originalTable.endIndex;
            /* Find children thead and tbody.
             * Only to process the immediate tr-children. Bugfix for inner tables
             */
            var thtb = this.originalTable.el.children();
            if (this.options.excludeFooter) {
                thtb = thtb.not('tfoot');
            }
            if (from < to) {
                for (i = from; i < to; i++) {
                    col1 = thtb.find('> tr > td:nth-child(' + i + ')')
                        .add(thtb.find('> tr > th:nth-child(' + i + ')'));
                    col2 = thtb.find('> tr > td:nth-child(' + (i + 1) + ')')
                        .add(thtb.find('> tr > th:nth-child(' + (i + 1) + ')'));
                    for (j = 0; j < col1.length; j++) {
                        swapNodes(col1[j], col2[j]);
                    }
                }
            } else {
                for (i = from; i > to; i--) {
                    col1 = thtb.find('> tr > td:nth-child(' + i + ')')
                        .add(thtb.find('> tr > th:nth-child(' + i + ')'));
                    col2 = thtb.find('> tr > td:nth-child(' + (i - 1) + ')')
                        .add(thtb.find('> tr > th:nth-child(' + (i - 1) + ')'));
                    for (j = 0; j < col1.length; j++) {
                        swapNodes(col1[j], col2[j]);
                    }
                }
            }
        },
        _rearrangeTableBackroundProcessing: function () {
            var _this = this;
            return function () {
                _this._bubbleCols();
                _this.options.beforeStop(_this.originalTable);
                _this.sortableTable.el.remove();
                restoreTextSelection();
                // persist state if necessary
                if (_this.options.persistState !== null) {
                    $.isFunction(_this.options.persistState) ? _this.options.persistState(_this.originalTable) : _this.persistState();
                }
            };
        },
        _rearrangeTable: function () {
            var _this = this;
            return function () {
                // remove handler-class -> handler is now finished
                _this.originalTable.selectedHandle.removeClass('dragtable-handle-selected');
                // add disabled class -> reorgorganisation starts soon
                _this.sortableTable.el.sortable("disable");
                _this.sortableTable.el.addClass('dragtable-disabled');
                _this.options.beforeReorganize(_this.originalTable, _this.sortableTable);
                // do reorganisation asynchronous
                // for chrome a little bit more than 1 ms because we want to force a rerender
                _this.originalTable.endIndex = _this.sortableTable.movingRow.prevAll().length + 1;
                setTimeout(_this._rearrangeTableBackroundProcessing(), 50);
            };
        },
        /*
         * Disrupts the table. The original table stays the same.
         * But on a layer above the original table we are constructing a list (ul > li)
         * each li with a separate table representig a single col of the original table.
         */
        _generateSortable: function (e) {
            !e.cancelBubble && (e.cancelBubble = true);
            var _this = this;
            // table attributes
            var attrs = this.originalTable.el[0].attributes;
            var attrsString = '';
            for (var i = 0; i < attrs.length; i++) {
                if (attrs[i].nodeValue && attrs[i].nodeName != 'id' && attrs[i].nodeName != 'width') {
                    attrsString += attrs[i].nodeName + '="' + attrs[i].nodeValue + '" ';
                }
            }

            // row attributes
            var rowAttrsArr = [];
            //compute height, special handling for ie needed :-(
            var heightArr = [];
            this.originalTable.el.find('tr').slice(0, this.options.maxMovingRows).each(function (i, v) {
                // row attributes
                var attrs = this.attributes;
                var attrsString = "";
                for (var j = 0; j < attrs.length; j++) {
                    if (attrs[j].nodeValue && attrs[j].nodeName != 'id') {
                        attrsString += " " + attrs[j].nodeName + '="' + attrs[j].nodeValue + '"';
                    }
                }
                rowAttrsArr.push(attrsString);
                heightArr.push($(this).height());
            });

            // compute width, no special handling for ie needed :-)
            var widthArr = [];
            // compute total width, needed for not wrapping around after the screen ends (floating)
            var totalWidth = 0;
            /* Find children thead and tbody.
             * Only to process the immediate tr-children. Bugfix for inner tables
             */
            var thtb = _this.originalTable.el.children();
            if (this.options.excludeFooter) {
                thtb = thtb.not('tfoot');
            }
            thtb.find('> tr > th').each(function (i, v) {
                var w = $(this).is(':visible') ? $(this).outerWidth() : 0;
                widthArr.push(w);
                totalWidth += w;
            });
            if (_this.options.exact) {
                var difference = totalWidth - _this.originalTable.el.outerWidth();
                widthArr[0] -= difference;
            }
            // one extra px on right and left side
            totalWidth += 2

            var sortableHtml = '<ul class="dragtable-sortable" style="position:absolute; width:' + totalWidth + 'px;">';
            // assemble the needed html
            thtb.find('> tr > th').each(function (i, v) {
                var width_li = $(this).is(':visible') ? $(this).outerWidth() : 0;
                sortableHtml += '<li style="width:' + width_li + 'px;">';
                sortableHtml += '<table ' + attrsString + '>';
                var row = thtb.find('> tr > th:nth-child(' + (i + 1) + ')');
                if (_this.options.maxMovingRows > 1) {
                    row = row.add(thtb.find('> tr > td:nth-child(' + (i + 1) + ')').slice(0, _this.options.maxMovingRows - 1));
                }
                row.each(function (j) {
                    // TODO: May cause duplicate style-Attribute
                    var row_content = $(this).clone().wrap('<div></div>').parent().html();
                    if (row_content.toLowerCase().indexOf('<th') === 0) sortableHtml += "<thead>";
                    sortableHtml += '<tr ' + rowAttrsArr[j] + '" style="height:' + heightArr[j] + 'px;">';
                    sortableHtml += row_content;
                    if (row_content.toLowerCase().indexOf('<th') === 0) sortableHtml += "</thead>";
                    sortableHtml += '</tr>';
                });
                sortableHtml += '</table>';
                sortableHtml += '</li>';
            });
            sortableHtml += '</ul>';
            this.sortableTable.el = this.originalTable.el.before(sortableHtml).prev();
            // set width if necessary
            this.sortableTable.el.find('> li > table').each(function (i, v) {
                $(this).css('width', widthArr[i] + 'px');
            });

            // assign this.sortableTable.selectedHandle
            this.sortableTable.selectedHandle = this.sortableTable.el.find('th .dragtable-handle-selected');

            var items = !this.options.dragaccept ? 'li' : 'li:has(' + this.options.dragaccept + ')';
            this.sortableTable.el.sortable({
                items: items,
                stop: this._rearrangeTable(),
                // pass thru options for sortable widget
                revert: this.options.revert,
                tolerance: this.options.tolerance,
                containment: this.options.containment,
                cursor: this.options.cursor,
                cursorAt: this.options.cursorAt,
                distance: this.options.distance,
                axis: this.options.axis
            });

            // assign start index
            this.originalTable.startIndex = $(e.target).closest('th').prevAll().length + 1;

            this.options.beforeMoving(this.originalTable, this.sortableTable);
            // Start moving by delegating the original event to the new sortable table
            this.sortableTable.movingRow = this.sortableTable.el.find('> li:nth-child(' + this.originalTable.startIndex + ')');

            // prevent the user from drag selecting "highlighting" surrounding page elements
            disableTextSelection();
            // clone the initial event and trigger the sort with it
            this.sortableTable.movingRow.trigger($.extend($.Event(e.type), {
                which: 1,
                clientX: e.clientX,
                clientY: e.clientY,
                pageX: e.pageX,
                pageY: e.pageY,
                screenX: e.screenX,
                screenY: e.screenY
            }));

            // Some inner divs to deliver the posibillity to style the placeholder more sophisticated
            var placeholder = this.sortableTable.el.find('.ui-sortable-placeholder');
            if (!placeholder.height() <= 0) {
                placeholder.css('height', this.sortableTable.el.find('.ui-sortable-helper').height());
            }

            placeholder.html('<div class="outer" style="height:100%;"><div class="inner" style="height:100%;"></div></div>');
        },
        bindTo: {},
        _create: function () {
            this.originalTable = {
                el: this.element,
                selectedHandle: $(),
                sortOrder: {},
                startIndex: 0,
                endIndex: 0
            };
            // bind draggable to 'th' by default
            this.bindTo = this.originalTable.el.find('th');
            // filter only the cols that are accepted
            if (this.options.dragaccept) {
                this.bindTo = this.bindTo.filter(this.options.dragaccept);
            }
            // bind draggable to handle if exists
            if (this.bindTo.find(this.options.dragHandle).length > 0) {
                this.bindTo = this.bindTo.find(this.options.dragHandle);
            }
            // restore state if necessary
            if (this.options.restoreState !== null) {
                $.isFunction(this.options.restoreState) ? this.options.restoreState(this.originalTable) : this._restoreState(this.options.restoreState);
            }
            var _this = this;
            this.bindTo.mousedown(function (evt) {
                // listen only to left mouse click
                if (evt.which !== 1) return;
                if (_this.options.beforeStart(_this.originalTable) === false) {
                    return;
                }
                clearTimeout(this.downTimer);
                this.downTimer = setTimeout(function () {
                    _this.originalTable.selectedHandle = $(this);
                    _this.originalTable.selectedHandle.addClass('dragtable-handle-selected');
                    _this._generateSortable(evt);
                }, _this.options.clickDelay);
            }).mouseup(function (evt) {
                clearTimeout(this.downTimer);
            });
        },
        redraw: function () {
            this.destroy();
            this._create();
        },
        destroy: function () {
            this.bindTo.unbind('mousedown');
            $.Widget.prototype.destroy.apply(this, arguments); // default destroy
            // now do other stuff particular to this widget
        }
    });

    /** closure-scoped "private" functions **/

    var body_onselectstart_save = $(document.body).attr('onselectstart'),
        body_unselectable_save = $(document.body).attr('unselectable');

    // css properties to disable user-select on the body tag by appending a <style> tag to the <head>
    // remove any current document selections

    function disableTextSelection() {
        // jQuery doesn't support the element.text attribute in MSIE 8
        // http://stackoverflow.com/questions/2692770/style-style-textcss-appendtohead-does-not-work-in-ie
        var $style = $('<style id="__dragtable_disable_text_selection__" type="text/css">body { -ms-user-select:none;-moz-user-select:-moz-none;-khtml-user-select:none;-webkit-user-select:none;user-select:none; }</style>');
        $(document.head).append($style);
        $(document.body).attr('onselectstart', 'return false;').attr('unselectable', 'on');
        if (window.getSelection) {
            window.getSelection().removeAllRanges();
        } else {
            document.selection.empty(); // MSIE http://msdn.microsoft.com/en-us/library/ms535869%28v=VS.85%29.aspx
        }
    }

    // remove the <style> tag, and restore the original <body> onselectstart attribute

    function restoreTextSelection() {
        $('#__dragtable_disable_text_selection__').remove();
        if (body_onselectstart_save) {
            $(document.body).attr('onselectstart', body_onselectstart_save);
        } else {
            $(document.body).removeAttr('onselectstart');
        }
        if (body_unselectable_save) {
            $(document.body).attr('unselectable', body_unselectable_save);
        } else {
            $(document.body).removeAttr('unselectable');
        }
    }

    function swapNodes(a, b) {
        var aparent = a.parentNode;
        var asibling = a.nextSibling === b ? a : a.nextSibling;
        b.parentNode.insertBefore(a, b);
        aparent.insertBefore(b, asibling);
    }
})(jQuery);

// dragtable end

function camelize(str) {
    return str.replace(/(?:^\w|[A-Z]|\b\w)/g, function (word, index) {
        return index === 0 ? word.toLowerCase() : word.toUpperCase();
    }).replace(/\s+/g, '');
}

/**
 * Returns the column index by the specified column name.
 * @param {string} tableId
 * @param {string} name
 */
function getColumnIndexByName(tableId, name) {

    var $tableColumns = $(`#${tableId} thead th`);
    var total = $tableColumns.length;

    for (var i = 0; i < total; i++) {
        var $column = $($tableColumns[i]);
        var columnName = $column.data('name');
        if (columnName == name) {
            return i;
        }
    }
    return -1;
}

function getColumnNamesByCurrentOrder(tableId) {

    var result = [];
    var $tableColumns = $(`#${tableId} thead th`);
    var total = $tableColumns.length;

    for (var i = 0; i < total; i++) {
        var $column = $($tableColumns[i]);
        var columnName = $column.data('name');
        result.push(columnName);
    }
    return result;
}

function getSelectedRow(tableId) {

    var $selectedRow = $(`#${tableId} tbody tr.selected`);
    return $selectedRow;
}

function getSelectedItemId(tableId) {

    var $selectedRow = getSelectedRow(tableId);
    return $selectedRow.data('id');
}

function getSelectedItemData(tableId) {
    var id = getSelectedItemId(tableId);
    var result = getItemData(tableId, id);
    return result;
}

function getItemData(tableId, id) {
    var json = document[`json_${tableId}`];
    var selectedItems = json.filter(x => x.id == id);
    if (selectedItems.length > 0) {
        return selectedItems[0];
    }
    return null;
}
function preparePhotoColumn($col, item) {
    if (item.photo && item.photo !== '') {
        return `<img src="${item.photo}" alt="Photo" style="width: 50px; height: 50px;" />`;
    } else {
        return '';
    }
}

function _getJsonValueForColumn($col, jsonItem) {
    var cName = $col.data('name');
    var val = '';

    if (!cName) {
        return val; // ako nema data-name, vrati prazan string odmah
    }

    if (cName === "_options_") {
        var optNames = ($col.data('options') || '').split(',');
        var optIcons = ($col.data('icons') || '').split(',');
        var optColors = ($col.data('colors') || '').split(',');

        for (let optionIndex in optNames) {
            var optionName = optNames[optionIndex];
            var optionIcon = optIcons[optionIndex];
            var optionColor = optColors[optionIndex];
            val += `<button type="button" name="${optionName}" class="align-items-center justify-content-center"><i class="fa ${optionIcon}" style="color:${optionColor};"></i></button>`;
        }
    }
    else {
        if (typeof cName === "string" && cName.indexOf('.') > 0) {
            var propSegments = cName.split('.');
            var complexTypeName = camelize(propSegments[0]);
            var complexItem = jsonItem[complexTypeName];

            if (complexItem != null) {
                cName = camelize(propSegments[1]);
                val = complexItem[cName];
            }
        }
        else {
            cName = camelize(cName);
            val = jsonItem[cName];
        }

        if (val == null) {
            val = '';
        }
        else if (typeof val === "boolean") {
            if (val) {
                val = '<i class="fa fa-check-square" style="color:green"></i>'
            }
            else {
                val = '<i class="fa fa-times" style="color:red"></i>'
            }
        }
    }
    return val;
}

function exportToExcel(tableId, outputFileName) {
    var tableData = [];
    var table = document.getElementById(tableId);

    // 1️⃣ Dodaj header (prvi red iz thead)
    var headerRow = table.tHead.rows[0];
    var headerData = [];
    for (var j = 0; j < headerRow.cells.length; j++) {
        headerData.push(headerRow.cells[j].innerText);
    }
    tableData.push(headerData);

    // 2️⃣ Pokupi sve aktivne filtere (multi-select)
    var filters = {};
    $(table).find('thead .column-filter').each(function () {
        var columnName = $(this).data('column');
        var selectedValues = $(this).val(); // niz vrednosti ili null

        if (selectedValues && selectedValues.length > 0) {
            // Pretvori sve u lowercase radi lakše provere
            filters[columnName] = selectedValues.map(v => v.toLowerCase());
        }
    });

    // 3️⃣ Iteriraj kroz tbody i dodaj samo redove koji zadovoljavaju filter
    $(table.tBodies[0].rows).each(function () {
        var row = this;
        var include = true;

        for (let col in filters) {
            var cellText = $(row).find(`td.${col}`).text().toLowerCase();
            var selectedValues = filters[col];

            // Ako ćelija nije u izabranim vrednostima, preskoči red
            if (!selectedValues.includes(cellText)) {
                include = false;
                break;
            }
        }

        if (include) {
            var rowData = [];
            for (var j = 0; j < row.cells.length; j++) {
                rowData.push(row.cells[j].innerText);
            }
            tableData.push(rowData);
        }
    });

    // 4️⃣ Generiši Excel fajl
    var wb = XLSX.utils.book_new();
    var ws = XLSX.utils.aoa_to_sheet(tableData);
    XLSX.utils.book_append_sheet(wb, ws, "Sheet1");
    XLSX.writeFile(wb, outputFileName + ".xlsx");
}
function exportToExcelHomePage(tableId, outputFileName) {
    var tableData = [];
    var table = document.getElementById(tableId);

    // 1️⃣ Dodaj prvi red iz thead kao header
    var headerRow = table.tHead.rows[0]; // prvi red u thead
    var headerData = [];
    for (var j = 0; j < headerRow.cells.length; j++) {
        headerData.push(headerRow.cells[j].innerText);
    }
    tableData.push(headerData);

    // 2️⃣ Iteriraj kroz tbody i dodaj samo vidljive redove
    var tbodyRows = table.tBodies[0].rows;
    for (var i = 0; i < tbodyRows.length; i++) {
        var row = tbodyRows[i];

        // Preskoči redove koji su sakriveni
        if (row.classList.contains("hidden-row") || row.style.display === "none") continue;

        var rowData = [];
        var cells = row.cells;
        for (var j = 0; j < cells.length; j++) {
            rowData.push(cells[j].innerText);
        }
        tableData.push(rowData);
    }

    // 3️⃣ Kreiraj workbook i dodaj sheet
    var wb = XLSX.utils.book_new();
    var ws = XLSX.utils.aoa_to_sheet(tableData);
    XLSX.utils.book_append_sheet(wb, ws, "Sheet1");

    // 4️⃣ Sačuvaj fajl i pokreni download
    XLSX.writeFile(wb, outputFileName);
}
function getPageSize(tableId) {
    var $rowsCountSelect = $(`#rows-count-${tableId}`);

    var pageSize = -1;
    if ($rowsCountSelect.length > 0) {
        pageSize = $rowsCountSelect.val();
    }
    return parseInt(pageSize, 10);
}

function replaceAttachmentIcon(tableId) {
    $(`#${tableId} tbody tr`).each(function () {
        var iconCell = $(this).find('td.HasDocuments i');
        if (iconCell.hasClass('fa-check-square')) {
            iconCell.removeClass('fa-check-square').addClass('fa-paperclip');
        } else if (iconCell.hasClass('fa-times')) {
            iconCell.removeClass('fa-times').addClass('fa-ban');
        }
    });
}
function requiredSelect2(fieldId) {
    var field = $(`#${fieldId}`)
    var id = field.val();
    var selectedIndex = field.prop('selectedIndex');
    if (selectedIndex === 0 || id == "") {
        field.siblings('.select2-container').addClass('is-invalid');
    }
    else {
        field.siblings('.select2-container').removeClass('is-invalid');
    }
}

function loadJson(tableId, jsonData, localizedPagination) {
    var $tableColumns = $(`#${tableId} thead tr:first-child th`);
    var $tableBody = $(`#${tableId} tbody`);

    var pageSize = getPageSize(tableId);
    var tBodyHtml = '';

    document[`json_${tableId}`] = jsonData;

    jsonData.forEach(function (item, rowIndex) {

        if (tBodyHtml == '') {
            tBodyHtml = `<tr class='selected' data-id='${item.id}'>`;
        }
        else {
            var isHiddenRow = pageSize != -1 && rowIndex >= pageSize;

            if (isHiddenRow) {
                tBodyHtml += `<tr class='hidden-row' data-id='${item.id}'>`;
            }
            else {
                tBodyHtml += `<tr data-id='${item.id}'>`;
            }
        }

        $tableColumns.each(function (__, columnElement) {
            var $col = $(columnElement);
            var prepareContentCallback = $col.data('prepare');
            var val = '';

            if (prepareContentCallback != null && prepareContentCallback.length > 0) {
                val = window[prepareContentCallback]($col, item);
            }
            else {
                val = _getJsonValueForColumn($col, item);
            }

            var cellAttr = '';
            var colName = $col.data('name');
            if ($col.hasClass("hidden-column")) {
                cellAttr = ' class="hidden-column ' + colName + '" ';
            }
            else {
                cellAttr = ' class="' + colName + '" ';
            }
            //if ($col.hasClass("hidden-column")) {
            //    cellAttr = ' class="hidden-column" ';
            //}
            var cellStyle = $col.data('cell-style');

            if (typeof cellStyle !== 'undefined' && cellStyle != null && cellStyle != '') {
                cellAttr += ` style="${cellStyle}" `;
            }

            tBodyHtml += '<td' + cellAttr + '>' + val + '</td>';
        });
        tBodyHtml += '</tr>';
    });

    $tableBody.html(tBodyHtml);

    renderPagingPanel(tableId, pageSize, 1, localizedPagination);

    populateFilters(tableId, jsonData);
}

function populateFilters(tableId, jsonData) {
    function camelize(str) {
        return str.charAt(0).toLowerCase() + str.slice(1);
    }

    var $filters = $(`#${tableId} thead tr.column-filters select.column-filter`);

    $filters.each(function () {
        var $select = $(this);
        var columnName = $select.data('column');

        // Očisti prethodne opcije osim prve "All"
        $select.find('option:not(:first)').remove();

        var valuesSet = new Set();

        jsonData.forEach(function (item) {
            var val = item[camelize(columnName)];
            console.log('Filter:', columnName, 'Value:', val);

            if (val != null && val !== '') {
                valuesSet.add(val.toString());
            }
        });

        var sortedValues = Array.from(valuesSet).sort();

        sortedValues.forEach(function (val) {
            $select.append(`<option value="${val}">${val}</option>`);
        });
    });
}

function searchPagedTable(tableId, searchUrl, additionalParams, localizedPagination, transformCallback) {
    var table = document.getElementById(tableId);
    debugger
    var pageNumber = 1; // Uvek postavljamo stranicu na 1
    var pageSize = getPageSize(tableId);
    var sortBy = table.getAttribute('data-last-sorted-column');
    var sortDirection = table.getAttribute('data-sort-direction');

    if (sortBy == -1) {
        sortBy = '';
    }

    var data = Object.assign({
        PageNumber: pageNumber,
        PageSize: pageSize,
        SortBy: sortBy,
        SortDirection: sortDirection
    }, additionalParams);

    $.ajax({
        url: searchUrl,
        method: "POST",
        data: data,
        success: function (response) {
            // 👇 Pozovi transformCallback ako je prosleđen
            if (typeof transformCallback === 'function') {
                response = transformCallback(response);
            }

            loadPagedJson(tableId, response, localizedPagination);
        }
    });
}

function loadPagedJson(tableId, jsonData, localizedPagination) {
    var $tableColumns = $(`#${tableId} thead tr:first-child th`);
    var $tableBody = $(`#${tableId} tbody`);

    var tBodyHtml = '';

    var payload = jsonData.payload;

    document[`json_${tableId}`] = payload;

    payload.forEach(function (item, rowIndex) {

        if (tBodyHtml == '') {
            tBodyHtml = `<tr class='selected' data-id='${item.id}'>`;
        }
        else {
            tBodyHtml += `<tr data-id='${item.id}'>`;
        }

        $tableColumns.each(function (__, columnElement) {
            var $col = $(columnElement);
            var prepareContentCallback = $col.data('prepare');
            var val = '';

            if (prepareContentCallback != null && prepareContentCallback.length > 0) {
                val = window[prepareContentCallback]($col, item);
            }
            else {
                val = _getJsonValueForColumn($col, item);
            }

            var cellAttr = '';
            var colName = $col.data('name');
            if ($col.hasClass("hidden-column")) {
                cellAttr = ' class="hidden-column ' + colName + '" ';
            }
            else {
                cellAttr = ' class="' + colName + '" ';
            }
            var cellStyle = $col.data('cell-style');

            if (typeof cellStyle !== 'undefined' && cellStyle != null && cellStyle != '') {
                cellAttr += ` style="${cellStyle}" `;
            }

            tBodyHtml += '<td' + cellAttr + '>' + val + '</td>';
        });
        tBodyHtml += '</tr>';
    });

    $tableBody.html(tBodyHtml);

    renderServerPagingPanel(tableId, jsonData.totalRowCount, localizedPagination);

    // Ovde popuni dropdown filtere na osnovu trenutnog payload-a
    populateFiltersPaged(tableId, payload);
}

function populateFiltersPaged(tableId, jsonData) {
    function camelize(str) {
        return str.charAt(0).toLowerCase() + str.slice(1);
    }

    var $filters = $(`#${tableId} thead tr.column-filters select.column-filter`);

    $filters.each(function () {
        var $select = $(this);
        var columnName = $select.data('column');

        // Sačuvaj trenutnu vrednost dropdown-a
        var currentVal = $select.val();

        var valuesSet = new Set();

        jsonData.forEach(function (item) {
            var val = item[camelize(columnName)];
            if (val != null && val !== '') {
                valuesSet.add(val.toString());
            }
        });

        var sortedValues = Array.from(valuesSet).sort();

        // Dodaj nove opcije koje još nisu u dropdownu
        sortedValues.forEach(function (val) {
            if ($select.find(`option[value="${val}"]`).length === 0) {
                $select.append(`<option value="${val}">${val}</option>`);
            }
        });

        // Vrati prethodno izabranu vrednost ako postoji, ili default na "All"
        if (currentVal && $select.find(`option[value="${currentVal}"]`).length) {
            $select.val(currentVal);
        } else {
            $select.val('');
        }
    });
}
function renderServerPagingPanel(tableId, totalRowCount, localizedPagination) {
    var pageIndex = Number(sessionStorage.getItem('pageIndex-' + tableId)) || 1;
    var pageSize = getPageSize(tableId);
    var jsonData = document[`json_${tableId}`];
    var $pagingContent = $(`#paging-${tableId}`);
    var rowsCount = totalRowCount;
    var pagingHtml = "";
    var serbizizedRows = localizedPagination.rows;

    if (localizedPagination.rows === 'redova' || localizedPagination.rows === 'редова') {
        switch (rowsCount.toString().slice(-1)) {
            case '1':
                if (rowsCount != 11) {
                    serbizizedRows = localizedPagination.rows1
                };
                break;
            case '2':
            case '3':
            case '4':
                if (rowsCount != 12 && rowsCount != 13 && rowsCount != 14) {
                    serbizizedRows = localizedPagination.rows234
                };
                break;
            default:
                break;
        }
    }
    else if (localizedPagination.rows === 'rows') {
        switch (rowsCount) {
            case 1:
                serbizizedRows = localizedPagination.rows1;
                break;
            default:
                break;
        }
    }

    if (rowsCount > 0) {
        var pagesCount = Math.floor(rowsCount / pageSize);
        var moduo = rowsCount % pageSize;

        if (moduo > 0) {
            pagesCount += 1;
        }

        if (pagesCount > 1) {

            if (pageIndex > 1) {
                pagingHtml += getPageButtonHtml(false, false, 1, localizedPagination.first);
            }

            var hasPrevPage = pageIndex > 1;
            if (hasPrevPage) {
                pagingHtml += getPageButtonHtml(false, true, pageIndex - 1, pageIndex - 1);
            }

            pagingHtml += getPageButtonHtml(true, false, pageIndex, pageIndex);

            var pagesCountToEnd = pagesCount - pageIndex;

            if (pagesCountToEnd > 0) {

                for (var i = 1; i < 5; i++) {
                    if (pagesCountToEnd - i >= 0) {
                        pagingHtml += getPageButtonHtml(false, false, pageIndex + i, pageIndex + i);
                    }
                }

                if (pagesCountToEnd > 3) {
                    pagingHtml += getPageButtonHtml(false, false, pagesCount, localizedPagination.last);
                }
            }

            var startIndex = (pageIndex - 1) * pageSize + 1;
            var pageRowsCount = rowsCount - startIndex;
            var endIndex = 0;

            if (pageRowsCount > pageSize) {
                pageRowsCount = pageSize;
                endIndex = startIndex + pageRowsCount - 1;
            }
            else {
                endIndex = startIndex + pageRowsCount;
            }
            $(`#${tableId}_wrapper .dataTables_info`).html(`${localizedPagination.showing} ${startIndex} ${localizedPagination.to} ${endIndex} ${localizedPagination.of} ${rowsCount} ${serbizizedRows}`);
        }
        else {
            $(`#${tableId}_wrapper .dataTables_info`).html(`${localizedPagination.showing} ${rowsCount} ${serbizizedRows}`);
        }
    }
    else {
        $(`#${tableId}_wrapper .dataTables_info`).html(`${localizedPagination.noEntries}`);
    }
    $pagingContent.html(pagingHtml);
}
function renderPagingPanel(tableId, pageSize, pageIndex, localizedPagination, totalRows) {
    var jsonData = document[`json_${tableId}`];
    var $pagingContent = $(`#paging-${tableId}`);

    // Ako totalRows nije prosleđen, fallback na ceo dataset
    totalRows = totalRows || jsonData.length;

    var pagingHtml = "";
    var serbizizedRows = localizedPagination.rows;

    // Serbizacija redova (srpski jezik)
    if (localizedPagination.rows === 'redova' || localizedPagination.rows === 'редова') {
        switch (totalRows.toString().slice(-1)) {
            case '1':
                if (totalRows != 11) serbizizedRows = localizedPagination.rows1;
                break;
            case '2':
            case '3':
            case '4':
                if (![12, 13, 14].includes(totalRows)) serbizizedRows = localizedPagination.rows234;
                break;
            default:
                break;
        }
    } else if (localizedPagination.rows === 'rows') {
        if (totalRows === 1) serbizizedRows = localizedPagination.rows1;
    }

    if (totalRows > 0) {
        // Paging logika
        var pagesCount = Math.floor(totalRows / pageSize);
        if (totalRows % pageSize > 0) pagesCount += 1;

        if (pagesCount > 1) {
            if (pageIndex > 1) {
                pagingHtml += getPageButtonHtml(false, false, 1, localizedPagination.first);
                pagingHtml += getPageButtonHtml(false, true, pageIndex - 1, pageIndex - 1);
            }

            pagingHtml += getPageButtonHtml(true, false, pageIndex, pageIndex);

            var pagesCountToEnd = pagesCount - pageIndex;
            if (pagesCountToEnd > 0) {
                for (var i = 1; i < 5; i++) {
                    if (pagesCountToEnd - i >= 0) {
                        pagingHtml += getPageButtonHtml(false, false, pageIndex + i, pageIndex + i);
                    }
                }
                if (pagesCountToEnd > 3) {
                    pagingHtml += getPageButtonHtml(false, false, pagesCount, localizedPagination.last);
                }
            }

            // Update info tekst
            var startIndex = (pageIndex - 1) * pageSize + 1;
            var endIndex = Math.min(startIndex + pageSize - 1, totalRows);

            $(`#${tableId}_wrapper .dataTables_info`).html(
                `${localizedPagination.showing} ${startIndex} ${localizedPagination.to} ${endIndex} ${localizedPagination.of} ${totalRows} ${serbizizedRows}`
            );
        } else {
            // Samo jedna strana
            $(`#${tableId}_wrapper .dataTables_info`).html(`${localizedPagination.showing} ${totalRows} ${serbizizedRows}`);
        }
    } else {
        // Nema redova
        $(`#${tableId}_wrapper .dataTables_info`).html(`${localizedPagination.noEntries}`);
    }

    $pagingContent.html(pagingHtml);
}
function getPageButtonHtml(isCurrent, isPrevious, pageIndex, caption) {

    var acti = isCurrent ? " active" : "";
    var prev = isPrevious ? " previous" : "";
    var result = `<li class="paginate_button page-item${acti}${prev}"><a href="#" data-index="${pageIndex}" class="page-link">${caption}</a></li>`;
    return result;
}

function validateRequiredFields($inputsContainer, validationMsg) {
    var isValid = true;
    $("input:required,select:required", $inputsContainer).each(function (_, field) {
        if (!field.name || ['file', 'reset', 'submit', 'button'].indexOf(field.type) > -1) {
            return;
        }
        if (field.value == null || field.value == "") {

            Swal.fire({
                title: validationMsg,
                icon: 'warning',
                confirmButtonText: 'Ok'
            });

            isValid = false;
        }
    });
    return isValid;
}

function validateRequiredFieldsExcludingContainer($inputsContainer, $excludedInputsContainer, validationMsg) {
    var isValid = true;

    // Exclude inputs within #buDataContainer
    $("input:required, select:required", $inputsContainer).not($excludedInputsContainer.find("input, select")).each(function (_, field) {
        if (!field.name || ['file', 'reset', 'submit', 'button'].indexOf(field.type) > -1) {
            return;
        }
        if (field.value == null || field.value === "") {
            Swal.fire({
                title: validationMsg,
                icon: 'warning',
                confirmButtonText: 'Ok'
            });
            isValid = false;
        }
    });

    return isValid;
}
function serializeJSON($inputsContainer) {
    var obj = {};

    $("input,select,textarea", $inputsContainer).each(function (_, field) {

        if (!field.name || ['file', 'reset', 'submit', 'button'].indexOf(field.type) > -1) {
            return;
        }

        if (field.dataset.manser != undefined) {
            return;
        }

        else if (field.type === 'select-multiple') {
            var options = [];
            Array.prototype.slice.call(field.options).forEach(function (option) {
                if (!option.selected) return;
                options.push(option.value);
            });
            if (options.length) {
                obj[field.name] = options;
            }
            return;
        }

        // 🔥 DODATO: time input podrška
        else if (field.type === 'time') {
            obj[field.name] = field.value;
            return;
        }

        else if (field.dataset.date != undefined) {
            var inputDate = getDateTimeFromInput(field);
            obj[field.name] = getEncodedDateTime(inputDate);
        }

        else if (field.classList.contains('laterDefinedDatePicker')) {
            var inputDate = getDateTimeFromInput(field);
            obj[field.name] = getEncodedDateTime(inputDate);
        }

        else if (['checkbox', 'radio'].indexOf(field.type) > -1 && !field.checked) {
            return;
        }

        else if (!obj[field.name]) {
            if (field.classList.contains('decimal-text')) {
                obj[field.name] = getDecimalFromInputField(field);
            }
            else {
                obj[field.name] = field.value;
            }
        }
    });

    return obj;
}

function handleActionResponseMessage(response, confirmationCallback, error, savedMsg, successMessage) {
    var tempElement = document.createElement('div');
    tempElement.innerHTML = response.message;
    if (response.valid) {
        var msg;
        if (successMessage) {
            msg = successMessage;
        }
        else {
            //msg = 'Your work has been saved'
            msg = savedMsg
        }
        Swal.fire({
            icon: 'success',
            title: msg
        }).then((result) => {
            if (result.isConfirmed) {
                confirmationCallback();
            }
        });
    }
    else {
        Swal.fire({
            icon: 'error',
            title: error,
            text: tempElement.textContent
        });
    }
}

function getDateTimeFromInput(field) {

    var result = null;

    if (field.value != null && field.value != undefined && field.value.length > 0) {
        var parts = field.value.split(' ');
        if (parts.length > 0) {

            var month;
            var date;
            if (parts[0].includes('/')) {
                // 02/23/2023
                var dateParts = parts[0].split('/');
                month = parseInt(dateParts[0], 10) - 1;
                date = parseInt(dateParts[1], 10);
            }
            else if (parts[0].includes('.')) {
                // 23.02.2023
                var dateParts = parts[0].split('.');
                date = parseInt(dateParts[0], 10);
                month = parseInt(dateParts[1], 10) - 1;
            }

            var year = parseInt(dateParts[2], 10);

            var date = new Date(year, month, date, 0, 0, 0);

            // 02/23/2023 02:23 AM
            if (parts.length == 3) {
                var timeParts = parts[1].split(':');
                var hour = parseInt(timeParts[0], 10);
                var min = parseInt(timeParts[1], 10);

                if (parts[2].toLowerCase() == "pm") {
                    hour += 12;
                }

                date.setHours(hour);
                date.setMinutes(min);
            }
            result = date;
        }
    }
    return result;
}
function getDecimalFromInputField(field) {

    var result = null;

    if (field.value != null && field.value != undefined && field.value.length > 0) {
        // Remove all non-numeric characters except dots and commas
        let numericString = field.value.replace(/[^\d.,]/g, '');

        // Replace commas with dots to standardize the decimal separator
        numericString = numericString.replace(/,/g, '.');

        // Remove all dots that are not part of the decimal separator
        numericString = numericString.replace(/\.(?=.*\.)/g, '');

        // Convert to numeric value
        let numericValue = parseFloat(numericString);

        result = numericValue;
    }
    return result;
}
function getDecimal(input) {

    var result = null;
    var field = $(`#${input}`)
    if (field != null && field != undefined && field.val().length > 0) {
        // Remove all non-numeric characters except dots and commas
        let numericString = field.val().replace(/[^\d.,]/g, '');

        // Replace commas with dots to standardize the decimal separator
        numericString = numericString.replace(/,/g, '.');

        // Remove all dots that are not part of the decimal separator
        numericString = numericString.replace(/\.(?=.*\.)/g, '');

        // Convert to numeric value
        let numericValue = parseFloat(numericString);

        result = numericValue;
    }
    return result;
}

/**
 * Used for internal date time serialization. Returns the exact date and time from the input without any time zone offset.
 * @param {Date} dateAndTime
 */
function getEncodedDateTime(dateAndTime) {

    if (dateAndTime == null) {
        return "";
    }

    var year = dateAndTime.getFullYear();

    // beginning with 0 for January to 11 for December. The value must be incremented with 1 for .net compatibility
    var numValue = dateAndTime.getMonth();
    var monthFormatted = String(numValue + 1).padStart(2, '0');

    numValue = dateAndTime.getDate();
    var dateFormatted = String(numValue).padStart(2, '0');

    numValue = dateAndTime.getHours();
    var hourFormatted = String(numValue).padStart(2, '0');

    numValue = dateAndTime.getMinutes();
    var minFormatted = String(numValue).padStart(2, '0');

    var formatted = `${year}.${monthFormatted}.${dateFormatted}-${hourFormatted}:${minFormatted}`;
    return formatted;
}

function initDefaultDatePicker(datePickerElement) {
    new AirDatepicker(datePickerElement, {
        //dateFormat: "M/d/yyyy",
        dateFormat: "dd.MM.yyyy",
        minutesStep: 15
    });
}

$(document).ready(function () {

    $("input[type='text'][data-date='auto']").each(function (_, datePickerElement) {
        initDefaultDatePicker(datePickerElement);
    });

    $("#mainContainer").on("click", "button[name='side-details']", function () {

        var $modalSplitter = $(this).closest('.modal-splitter');

        var $sideView = $(".side-view", $modalSplitter);

        var $headerColumns = $("table thead th", $modalSplitter);
        var $dataCells = $(this).closest('tr').find('td');

        var sideHtml = '<table class="table-bordered"><tbody>';

        for (let i = 0; i < $headerColumns.length; i++) {

            var columnCaption = $($headerColumns[i]).text();
            if (columnCaption != "" && columnCaption != "Options") {
                sideHtml += '<tr><td>' + columnCaption + ':</td><td>' + $($dataCells[i]).html() + '</td></tr>';
            }
        }

        sideHtml += '</table></tbody>';

        $sideView.html(sideHtml);
        $sideView.show();
    });
});

function isInt(n) {
    return /^[+-]?\d+$/.test(n);
}

function isDecimal(n) {
    // Remove thousands separators (either . or , but not mixed) and handle optional decimal separator
    // Match numbers like: "1,234.56", "1.234,56", "1234,56", "1234.56", "1.234", "1,234", "1234", etc.
    const regex = /^[+-]?(\d{1,3}(,\d{3})*|\d{1,3}(\.\d{3})*)([.,]\d+)?$/;

    // Check if the string matches the pattern
    return regex.test(n);
}

function toDecimalNumber(str) {
    var decimalSeparator = (str.includes(',') && str.lastIndexOf(',') > str.lastIndexOf('.')) ? ',' : '.';
    var normalized = str.replace(/[,.](?=\d{3})/g, '');
    if (decimalSeparator === ',') {
        normalized = normalized.replace(',', '.');
    }
    var result = parseFloat(normalized);
    return isNaN(result) ? null : result;
}

function parseDateString(dateString) {
    const formats = [
        { regex: /^\d{4}-\d{2}-\d{2}$/, parser: str => parseISODate(str) },           // YYYY-MM-DD (ISO)
        { regex: /^\d{2}\/\d{2}\/\d{4}$/, parser: str => parseUSDate(str) },          // MM/DD/YYYY (en-US)
        { regex: /^\d{2}-\d{2}-\d{4}$/, parser: str => parseEUDate(str) },            // DD-MM-YYYY (European)
        { regex: /^\d{4}\/\d{2}\/\d{2}$/, parser: str => parseISODate(str) },         // YYYY/MM/DD (ISO)
        { regex: /^\d{1,2}\.\d{1,2}\.\d{4}$/, parser: str => parseDottedDate(str) }   // D.M.YYYY (sr-Latn, EU)
    ];

    for (let format of formats) {
        if (format.regex.test(dateString)) {
            return format.parser(dateString);
        }
    }

    throw new Error("Date string format not recognized.");
}

// Parser functions
function parseISODate(dateString) {
    // YYYY-MM-DD or YYYY/MM/DD
    const [year, month, day] = dateString.split(/[-\/]/).map(Number);
    return new Date(year, month - 1, day);
}

function parseUSDate(dateString) {
    // MM/DD/YYYY
    const [month, day, year] = dateString.split('/').map(Number);
    return new Date(year, month - 1, day);
}

function parseEUDate(dateString) {
    // DD-MM-YYYY
    const [day, month, year] = dateString.split('-').map(Number);
    return new Date(year, month - 1, day);
}

function parseDottedDate(dateString) {
    // D.M.YYYY
    const [day, month, year] = dateString.split('.').map(Number);
    return new Date(year, month - 1, day);
}

function isValidDate(dateString) {
    // Attempt to parse using the Date object (can handle ISO and some common formats)
    const parsedDate = new Date(dateString);
    if (!isNaN(parsedDate.getTime())) {
        return true;
    }

    // List of known locale-sensitive formats
    const formats = [
        'en-US', 'en-GB', 'fr-FR', 'de-DE', 'sr-Latn', 'es-ES', 'it-IT', 'ja-JP', 'zh-CN', 'ru-RU', 'sr-Cyrl'
        // Add more locales as needed
    ];

    // Attempt to parse using each locale format
    for (let i = 0; i < formats.length; i++) {
        const format = formats[i];
        try {
            const options = { year: 'numeric', month: 'numeric', day: 'numeric' };
            const dtf = new Intl.DateTimeFormat(format, options);
            const parts = dtf.formatToParts(new Date());

            // Construct regex based on parts and try to match the date string
            const regexParts = parts.map(part => {
                switch (part.type) {
                    case 'day': return '\\d{1,2}';
                    case 'month': return '\\d{1,2}';
                    case 'year': return '\\d{4}';
                    default: return '\\D+';
                }
            });
            const regex = new RegExp(`^${regexParts.join('')}$`);
            if (regex.test(dateString)) {
                return true;
            }
        } catch (e) {
            // Continue to next format if parsing fails
        }
    }

    // Fallbacks: ISO 8601, RFC 2822, and other common date formats
    const fallbackFormats = [
        /^\d{4}-\d{2}-\d{2}$/,            // YYYY-MM-DD (ISO)
        /^\d{2}\/\d{2}\/\d{4}$/,          // MM/DD/YYYY
        /^\d{2}-\d{2}-\d{4}$/,            // DD-MM-YYYY
        /^\d{4}\/\d{2}\/\d{2}$/,          // YYYY/MM/DD
        /^\d{1,2}\.\d{1,2}\.\d{4}$/       // D.M.YYYY
    ];

    for (let i = 0; i < fallbackFormats.length; i++) {
        if (fallbackFormats[i].test(dateString)) {
            return true;
        }
    }

    // If all parsing methods fail, return false
    return false;
}

function compareCellString(a, b, asc) {
    if (asc) {
        if (isInt(a) && isInt(b)) {
            return +a < +b
        }
        else if (isDecimal(a) && isDecimal(b)) {
            const dec1 = toDecimalNumber(a);
            const dec2 = toDecimalNumber(b);
            return +dec1 < +dec2
        }
        else if (isValidDate(a) && isValidDate(b)) {
            const date1 = parseDateString(a);
            const date2 = parseDateString(b);
            return date1 < date2
        }
        else {
            return a.toLowerCase() < b.toLowerCase();
        }
    }
    else {
        if (isInt(a) && isInt(b)) {
            return +a > +b
        }
        else if (isDecimal(a) && isDecimal(b)) {
            const dec1 = toDecimalNumber(a);
            const dec2 = toDecimalNumber(b);
            return +dec1 > +dec2
        }
        else if (isValidDate(a) && isValidDate(b)) {
            const date1 = parseDateString(a);
            const date2 = parseDateString(b);
            return date1 > date2
        }
        else {
            return a.toLowerCase() > b.toLowerCase();
        }
    }
}

function sortAjaxTableOld(columnIndex, table) {

    var rows = [];
    var switching = true;
    var dir = "asc";
    var i,
        shouldSwitch,
        switchcount = 0;

    // make a loop that will continue until no switching has been done:
    while (switching) {
        //start by saying: no switching is done:
        switching = false;
        rows = table.getElementsByTagName("TR");

        for (i = 1; i < rows.length - 2; i++) {

            var x = rows[i].getElementsByTagName("TD")[columnIndex];
            var y = rows[i + 1].getElementsByTagName("TD")[columnIndex];

            shouldSwitch = compareCellString(x.innerHTML.toLowerCase(), y.innerHTML.toLowerCase(), dir == "asc");

            if (shouldSwitch) {
                break;
            }
        }

        if (shouldSwitch) {
            // if a switch has been marked, make the switch and mark that a switch has been done:
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            // each time a switch is done, increase this count by 1:
            switchcount++;
        }
        else {
            // if no switching has been done AND the direction is "asc", set the direction to "desc" and run the while loop again
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}

function sortAjaxTable(columnIndex, table, tableId) {
    var lastSortedColumn = table.getAttribute('data-last-sorted-column');
    var sortDirection = table.getAttribute('data-sort-direction') || 'asc';

    if (columnIndex == lastSortedColumn) {
        sortDirection = sortDirection === 'asc' ? 'desc' : 'asc'; // Toggle direction
    } else {
        sortDirection = 'asc'; // Default to ascending for a new column
    }

    table.setAttribute('data-last-sorted-column', columnIndex);
    table.setAttribute('data-sort-direction', sortDirection);

    var tbody = table.querySelector('tbody');
    var rows = Array.from(tbody.getElementsByTagName("TR"));

    /* const totalsRow = rows.pop();*/

    rows.sort(function (rowA, rowB) {
        var cellA = rowA.getElementsByTagName("TD")[columnIndex].innerHTML.toLowerCase();
        var cellB = rowB.getElementsByTagName("TD")[columnIndex].innerHTML.toLowerCase();

        return compareCellString(cellA, cellB, sortDirection === 'asc') ? -1 : 1;
    });

    var pageSize = getPageSize(tableId);
    var fragment = document.createDocumentFragment();
    var rowNumber = 0;

    rows.forEach(function (row) {
        var newRow = row.cloneNode(true);
        if (rowNumber < pageSize) {
            newRow.classList.remove("hidden-row");
        } else {
            newRow.classList.add("hidden-row");
        }
        fragment.appendChild(newRow);
        rowNumber++;
    });
    while (tbody.firstChild) {
        tbody.removeChild(tbody.firstChild);
    }
    tbody.appendChild(fragment);
    /*tbody.appendChild(totalsRow); */
}

function sortAjaxPagedTable(columnIndex, table, tableId) {
    var lastSortedColumn = table.getAttribute('data-last-sorted-column');
    var sortDirection = table.getAttribute('data-sort-direction') || 'asc';


    var columnElement = table.querySelector(`thead th:nth-child(${columnIndex + 1})`);
    var datafieldName = columnElement.getAttribute('datafield-name');
    var columnName = datafieldName != "" ? datafieldName : columnElement.getAttribute('data-name');


    if (columnName == lastSortedColumn) {
        sortDirection = sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
        sortDirection = 'asc';
    }

    table.setAttribute('data-last-sorted-column', columnName);
    table.setAttribute('data-sort-direction', sortDirection);

}


function ajaxTableCalculateTotalBULE(tableId, fieldName, culture) {
    var total = 0;
    var json = document[`json_${tableId}`];

    // Calculate the total for each column
    json.forEach(function (item) {
        var val = item[fieldName];
        var entryType = item['entryTypeName'];
        var ledgerEntryType = item['ledgerEntryTypeName'];
        if (val !== undefined && val != null) {
            if (entryType == 'Potraživanje' || entryType == 'Isplata' || entryType == 'Obustava' || entryType == 'Odobrenje') {
                total = currency(total).add(val);
                //    total += val;
            } else {
                if (ledgerEntryType == 'Realizovane negativne kursne razlike') {
                    if (val > 0) {
                        total = currency(total).subtract(val);
                        //total -= val;
                    }
                    else {
                        total = currency(total).add(val);
                        //total += val;
                    }
                }
                else {
                    total = currency(total).subtract(val);
                    //total -= val;
                }
            }
        }
    });
    total = currency(total, { precision: 2 });
    total = currency(total, { decimal: ',', separator: '.', symbol: '' }).format();
    return total.toLocaleString(culture, { minimumFractionDigits: 2 });
}

function ajaxTableCalculateTotal(tableId, fieldName, culture) {
    var total = 0;
    var json = document[`json_${tableId}`];

    // Calculate the total for each column
    json.forEach(function (item) {
        var val = item[fieldName];

        if (val !== undefined && val != null) {
            total = currency(total).add(val);
            //total += item[fieldName];
        }
    });
    total = currency(total, { precision: 2 });
    total = currency(total, { decimal: ',', separator: '.', symbol: '' }).format();
    return total.toLocaleString(culture, { minimumFractionDigits: 2 });
}

function validateIdentificationNumber(field) {
    var identificationNumber = field.value.trim();

    if (identificationNumber === '') {
        return true;
    }
    if (identificationNumber.length !== 13) {
        return false;
    }

    var day = parseInt(identificationNumber.substring(0, 2), 10);
    if (day < 1 || day > 31) {
        return false;
    }

    var month = parseInt(identificationNumber.substring(2, 4), 10);
    if (month < 1 || month > 12) {
        return false;
    }

    var genderDigit = identificationNumber.charAt(4);
    if (genderDigit !== '0' && genderDigit !== '9') {
        return false;
    }

    var sum = 0;
    sum = parseInt(identificationNumber.substring(0, 1)) * 7 + parseInt(identificationNumber.substring(1, 2)) * 6 + parseInt(identificationNumber.substring(2, 3)) * 5 + parseInt(identificationNumber.substring(3, 4)) * 4
        + parseInt(identificationNumber.substring(4, 5)) * 3 + parseInt(identificationNumber.substring(5, 6)) * 2 + parseInt(identificationNumber.substring(6, 7)) * 7 + parseInt(identificationNumber.substring(7, 8)) * 6
        + parseInt(identificationNumber.substring(8, 9)) * 5 + parseInt(identificationNumber.substring(9, 10)) * 4 + parseInt(identificationNumber.substring(10, 11)) * 3 + parseInt(identificationNumber.substring(11, 12)) * 2
        + parseInt(identificationNumber.substring(12));
    if (sum % 11 != 0) {
        return false;
    }

    return true;
}

function identificationNumberCheck(textBoxId, message1, message2, message3, message4, message5) {
    let idValue = textBoxId.value;
    var digit12 = idValue.substring(0, 2);
    var digit34 = idValue.substring(2, 4);
    var digit5 = parseInt(idValue.substring(4, 5));
    var year = "";
    var tempElement = document.createElement('div');

    if (idValue.length == 13) {
        if (digit12 <= 0 || digit12 > 31) {
            highlight(textBoxId);
            tempElement.innerHTML = message1;
            alert(tempElement.textContent);
            setTimeout(() => { textBoxId.focus(); }, 0);
        }
        else {
            if (digit34 <= 0 || digit34 > 12) {
                highlight(textBoxId);
                tempElement.innerHTML = message2;
                alert(tempElement.textContent);
                setTimeout(() => { textBoxId.focus(); }, 0);
            }
            else {
                if (digit5 != 0 && digit5 != 9) {
                    highlight(textBoxId);
                    tempElement.innerHTML = message3;
                    alert(tempElement.textContent);
                    setTimeout(() => { textBoxId.focus(); }, 0);
                }
                else {
                    var sum = 0;
                    sum = parseInt(idValue.substring(0, 1)) * 7 + parseInt(idValue.substring(1, 2)) * 6 + parseInt(idValue.substring(2, 3)) * 5 + parseInt(idValue.substring(3, 4)) * 4
                        + parseInt(idValue.substring(4, 5)) * 3 + parseInt(idValue.substring(5, 6)) * 2 + parseInt(idValue.substring(6, 7)) * 7 + parseInt(idValue.substring(7, 8)) * 6
                        + parseInt(idValue.substring(8, 9)) * 5 + parseInt(idValue.substring(9, 10)) * 4 + parseInt(idValue.substring(10, 11)) * 3 + parseInt(idValue.substring(11, 12)) * 2
                        + parseInt(idValue.substring(12));
                    if (sum % 11 != 0) {
                        highlight(textBoxId);
                        tempElement.innerHTML = message4;
                        alert(tempElement.textContent);
                        setTimeout(() => { textBoxId.focus(); }, 0);
                    }
                    else {
                        if (digit5 == 0) {
                            year = "2" + idValue.substring(4, 7);
                        }
                        else {
                            year = "1" + idValue.substring(4, 7);
                        }
                        dateOfBirth.value = new Date(year + '-' + digit34 + '-' + digit12);
                        highlightnone(textBoxId);
                    }
                }
            }
        }
    }
    else {
        highlight(textBoxId);
        tempElement.innerHTML = message5;
        alert(tempElement.textContent);
        setTimeout(() => { textBoxId.focus(); }, 0);
    }

    function highlight(x) {
        x.style.borderColor = "red"
    }
    function highlightnone(x) {
        x.style.borderColor = ""
    }


}