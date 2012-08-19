var TKApp = Em.Application.create({
    ready: function () {
        // Call the superclass's `ready` method.
        this._super();

        TKApp.dataController.setContent(TK.Resource.Model.AllCategories);
        TKApp.resourceCategoriesController.setContent(TK.Resource.Model.Resource.Categories);
    }
});


TKApp.Category = Em.Object.extend({
    //TODO:

    Id: null,
    Name: null,
    ParentId: null,
    IsLast: false,
    Visable: true
});

TKApp.dataController = Em.ArrayController.create({
    content: []
    ,
    addItem: function (item) {
        this.insertAt(TKApp.dataController.content.length, item);

    },
    setContent: function (array) {
        for (i = 0; i < array.length; i++) {
            TKApp.dataController.addItem(TKApp.Category.create(array[i]));
        }
    },
    removeItem: function (propName, value) {
        var obj = this.findProperty(propName, value);
        this.removeObject(obj);
    }
});

TKApp.resourceCategoriesController = Em.ArrayController.create({
    content: []
    ,
    addItem: function (item) {
        this.insertAt(TKApp.resourceCategoriesController.content.length, item);

    },
    setContent: function (array) {
        for (i = 0; i < array.length; i++) {
            TKApp.resourceCategoriesController.addItem(TKApp.Category.create(array[i]));
        }
    },
    removeItem: function (propName, value) {
        var obj = this.findProperty(propName, value);
        this.removeObject(obj);
    }
});


TKApp.BreadCrumb = Em.View.extend({
    //TODO:
    attributeBindings: ['href'],
    href: function () {
        return this.get('content').get('Id');
    } .property(),
    tagName: 'li',
    click: function (event) {
        var cat = this.get('content');
        TKApp.resourceCategoriesController.addItem(cat);
        TKApp.dataController.removeItem('Id', cat.get('Id'));
    }
});

TKApp.BreadCrumb2 = Em.View.extend({
    //TODO:
    attributeBindings: ['href'],
    href: function () {
        return this.get('content').get('Id');
    } .property(),
    tagName: 'li',
    click: function (event) {
        var cat = this.get('content');
        TKApp.dataController.addItem(cat);
        TKApp.resourceCategoriesController.removeItem('Id', this.get('content').get('Id'));
    }
});
TKApp.HiddenInput = Em.View.extend({
    attributeBindings: ['value', 'type', 'name'],
    type: 'hidden',
    tagName: 'input',
    name: function () {
        var cat = this.get('content');
        return 'r.Categories[' + cat.get('Id') + '].Id';
    } .property()
});

TKApp.HiddenIndex = Em.View.extend({
    attributeBindings: ['value', 'type', 'name'],
    type: 'hidden',
    tagName: 'input',
    name: 'r.Categories.Index'
});