using UnityEngine;
using System.Collections;

interface IPanelBehavior{
    void Prepare();
    void IsLoaded();
    void AllowDragging(bool b);
    bool AllowSwipePrevious();
    bool AllowSwipeNext();
}
