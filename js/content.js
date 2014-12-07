var $content = $("#content");

function loadContent(path, uindex) {
    if(typeof(uindex)==='undefined') uindex = -1;

    $content.load('content/' + path + '.html', function() {
        MathJax.Hub.Queue(["Typeset", MathJax.Hub])
    });

    if(uindex >= 0) {
        u.getUnity().SendMessage("SceneManager", "ChangeScene", uindex);
    }
}

function sceneLoaded(status) {
    console.log("Loaded scene: " + status);
}