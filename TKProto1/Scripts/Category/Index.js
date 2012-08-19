var TKApp = Em.Application.create({
    ready: function () {
        // Call the superclass's `ready` method.
        this._super();

        TKApp.dataController.setContent(TK.Category.Model.BreadCrumbCategoryList);
    }
});




TKApp.Category = Em.Object.extend({
    //TODO:

    Id : null,
    Name: null,
    ParentId: null,
    IsLast: false
});


TKApp.dataController = Em.ArrayController.create({
    content: [],
    addItem: function (item) {
        this.insertAt(TKApp.dataController.content.length, item);

    },
    setContent: function (array) {
        for (i = 0; i < array.length; i++) {
            TKApp.dataController.addItem(TKApp.Category.create(array[i]));
        }
    }
});


TKApp.BreadCrumb = Em.View.extend({
    //TODO:
    attributeBindings: ['href'],
    href: function () {
        return this.get('content').get('Id');
    }.property(),
    tagName: 'a'
});


$(function () {

   //
});