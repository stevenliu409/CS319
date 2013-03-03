function addElementToParent(parentDiv, element, fadeEffect, speed) {
    if (fadeEffect == true) {
        parentDiv.append(element).children(':last').hide().fadeIn(speed);
    } else {
        parentDiv.append(element);
    }
}