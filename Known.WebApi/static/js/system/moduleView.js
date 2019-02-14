﻿var ModuleView = {

    tree: null,
    grid: null,
    formData: {
        Id: '',
        Enabled: 'Y'
    },

    show: function () {
        Toolbar.bind('tbModule', this);

        this.tree = mini.get('leftTree');
        this.tree.on('nodeselect', this.onTreeNodeSelect);
        this.tree.on('drop', this.onTreeDrop);

        this.grid = new Grid('Module');
        this.showGrid('0');
    },

    showGrid: function (pid) {
        var _this = this;
        this.formData.ParentId = pid;
        this.grid.query.pid.setValue(pid);
        this.grid.load(function (e) {
            _this.formData.Sort = e.result.total + 1;
        });
    },

    onTreeNodeSelect: function (e) {
        ModuleView.showGrid(e.node.id);
    },

    onTreeDrop: function (e) {
        if (e.dragAction !== 'add')
            return;

        Ajax.postJson('/Module/DropModule', {
            id: e.dragNode.id, pid: e.dragNode.pid
        }, function (res) {
            Message.result(res);
        });
    },

    //toolbar
    add: function () {
        this.showForm(this.formData);
    },

    copy: function () {
        var _this = this;
        this.grid.checkSelect(function (row) {
            var data = mini.clone(row);
            data.Id = '';
            data.Sort = _this.grid.getData().length + 1;
            _this.showForm(data);
        });
    },

    edit: function () {
        var _this = this;
        this.grid.checkSelect(function (row) {
            _this.showForm(row);
        });
    },

    remove: function () {
        var _this = this;
        this.grid.deleteRows(function (rows, data) {
            Ajax.postJson('/Module/DeleteModules', {
                data: data
            }, function (res) {
                Message.result(res, function () {
                    _this.tree.reload();
                    _this.grid.reload();
                });
            });
        });
    },

    //methods
    showForm: function (data) {
        var _this = this;
        Dialog.show({
            url: '/System/Partial', param: { name: 'ModuleForm' },
            title: '模块管理【' + (data.Id === '' ? '新增' : '编辑') + '】',
            width: 800, height: 450,
            callback: function () {
                ModuleForm.show({
                    data: data,
                    callback: function () {
                        _this.tree.reload();
                        _this.grid.reload();
                    }
                });
            }
        });
    }

};

var ModuleForm = {

    form: null,

    show: function (option) {
        Toolbar.bind('tbFormModule', this);

        this.form = new Form('formModule', {
            data: option.data,
            callback: function (f, d) {
                $('#moduleIcon').attr('class', 'mini-icon mini-iconfont ' + d.Icon);
            }
        });

        //this.form.Icon.on('drawcell', this.onIconDrawCell);
    },

    //control
    onIconDrawCell: function (e) {
        var item = e.record, field = e.field, value = e.value;
        e.cellHtml = '<span class="fa ' + value + '" style="width:16px;"> ' + value + '</span>';
    },

    //toolbar
    save: function () {
        var _this = this;
        this.form.saveData({
            url: '/Module/SaveModule',
            callback: function (data) {
                _this.form.setData(data, _this.option.callback);
            }
        });
    }

};

$(function () {
    mini.parse();
    ModuleView.show();
});