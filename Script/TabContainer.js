/* File Created: June 20, 2012 */
function PanelClick(sender, e) {
    var Messages = $get('<%=Messages.ClientID%>');
    Highlight(Messages);
}

function ActiveTabChanged(sender, e) {
    var CurrentTab = $get('<%=CurrentTab.ClientID%>');
    CurrentTab.innerHTML = sender.get_activeTab().get_headerText();
    Highlight(CurrentTab);
}

var HighlightAnimations = {};
function Highlight(el) {
    if (HighlightAnimations[el.uniqueID] == null) {
        HighlightAnimations[el.uniqueID] = AjaxControlToolkit.Animation.createAnimation({
            AnimationName: "color",
            duration: 0.5,
            property: "style",
            propertyKey: "backgroundColor",
            startValue: "#FFFF90",
            endValue: "#FFFFFF"
        }, el);
    }
    HighlightAnimations[el.uniqueID].stop();
    HighlightAnimations[el.uniqueID].play();
}  