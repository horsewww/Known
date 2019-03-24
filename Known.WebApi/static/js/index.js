﻿var Index = {

    mainTabs: null,

    show: function () {
        this.mainTabs = this._getMainTabs();
        this._initLeftTree();
    },

    _getMainTabs: function () {
        return $('#mainTabs').tabs({
            border: false,
            onSelect: function (title) {

            }
        });
    },

    _initLeftTree: function () {
        var _this = this, mainTabs = this.mainTabs;
        $('#leftTree').tree({
            method: 'get',
            url: 'static/data/menu.json',
            onClick: function (node) {
                if (node.children)
                    return;

                if (mainTabs.tabs('exists', node.text)) {
                    mainTabs.tabs('select', node.text);
                } else {
                    mainTabs.tabs('add', {
                        id: node.id,
                        title: node.text,
                        iconCls: node.iconCls,
                        href: '/Pages' + node.url,
                        closable: true,
                        bodyCls: 'content'
                    });
                }
            },
            onLoadSuccess: function (node, data) {
                _this._removeNodeIcon();
            },
            onExpand: function (node) {
                _this._removeNodeIcon();
            }
        });
    },

    _removeNodeIcon: function () {
        $('.fa').removeClass('tree-icon tree-file');
        $('.fa').removeClass('tree-icon tree-folder tree-folder-open tree-folder-closed');
    }

};

Index.show();