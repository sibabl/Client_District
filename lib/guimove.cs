$GUI::AnimationQuality = 62.5; // lowest possible
 
function GuiControl::moveTo(%this, %position, %duration, %easing, %callback, %param)
{
    if (%duration < 0)
    {
        error("ERROR: ::moveTo() - duration cannot be negative!");
        return 1;
    }
 
    if (%easing !$= "" && !isFunction(%easing))
    {
        error("ERROR: ::moveTo() - easing function does not exist!");
        return 1;
    }
 
    %x1 = getWord(%this.position, 0);
    %y1 = getWord(%this.position, 1);
    %x2 = getWord(%position, 0);
    %y2 = getWord(%position, 1);
 
    %this.updateMoveTo(%x1, %y1, %x2, %y2, 0, %duration, %easing, %callback, %param);
}
 
function GuiControl::updateMoveTo(%this, %x1, %y1, %x2, %y2,
    %elapsed, %duration, %easing, %param)
{
    cancel(%this.updateMoveTo);
    %factor = mClampF(%elapsed / %duration, 0, 1);
 
    if (%easing !$= "")
        %factor = call(%easing, %factor);
 
    %this.resize(
        %x1 + (%x2 - %x1) * %factor,
        %y1 + (%y2 - %y1) * %factor,
        getWord(%this.extent, 0),
        getWord(%this.extent, 1)
    );
 
    if (%elapsed < %duration)
        %this.updateMoveTo = %this.schedule(
            1000 / $GUI::AnimationQuality, updateMoveTo, %x1, %y1, %x2, %y2,
            %elapsed + 1 / $GUI::AnimationQuality, %duration, %easing, %callback, %param);
    else
        call(%callback, %this, %x1 SPC %y1);
}
 
function easeInQuad (%t) { return %t * %t; }
function easeInCubic(%t) { return %t * %t * %t; }
function easeInQuart(%t) { return %t * %t * %t * %t; }
function easeInQuint(%t) { return %t * %t * %t * %t * %t; }
function easeInSine (%t) { return -1 * mCos(%t * ($pi / 2)) + 1; }
function easeInExpo (%t) { return mPow(2, -10 * (%t - 1)); }
function easeInCirc (%t) { return -1 * (mSqrt(1 - %t * %t) - 1); }
 
function easeOutQuad (%t) { return -1 * %t * (%t - 2); }                 // //
function easeOutCubic(%t) { %t--; return %t * %t * %t + 1; }             // //
function easeOutQuart(%t) { %t--; return -1 * (%t * %t * %t * %t - 1); } // //
function easeOutQuint(%t) { %t--; return %t * %t * %t * %t * %t + 1; }   // //
function easeOutSine (%t) { return mSin(%t * ($pi / 2)); }               // works
function easeOutExpo (%t) { return -mPow(2, -10 * %t) + 1; }             // works
function easeOutCirc (%t) { %t--; return mSqrt(1 - %t * %t); }           // works
 
function easeInOutSine(%t)
{
    return -0.5 * (mCos($pi * %t) - 1);
}
 
function easeInOutExpo(%t)
{
    if (%t <= 0) return 0;
    if (%t >= 1) return 1;
 
    %t *= 2;
 
    if (%t < 1)
        return 0.5 * mPow(2, 10 * (%t - 1));
 
    return 0.5 * (-mPow(2, -10 * (%t - 1)) + 2);
}
 
function easeInOutCirc(%t)
{
    %t *= 2;
 
    if (%t < 1)
        return -0.5 * (mSqrt(1 - %t * %t) - 1);
 
    %t -= 2;
    return 0.5 * (mSqrt(1 - %t * %t) + 1);
}
 
function easeInOutBack(%t)
{
    %s = 1.70158;
    %t *= 2;
    %s *= 1.525;
 
    if (%t < 1)
        return 0.5 * (%t * %t * ((%s + 1) * %t - %s));
 
    %t -= 2;
    return 0.5 * (%t * %t * ((%s + 1) * %t + %s) + 2);
}
 
function easeOutBounce(%t)
{
    if (%t < 1 / 2.75)
        return 7.5625 * %t * %t;
    else if (%t < 2 / 2.75)
    {
        %t -= 1.5 / 2.75;
        return 7.5625 * %t * %t + 0.75;
    }
    else if (%t < 2.5 / 2.75)
    {
        %t -= 2.25 / 2.75;
        return 7.5625 * %t * %t + 0.9375;
    }
    else
    {
        %t -= 2.625 / 2.75;
        return 7.5625 * %t * %t + 0.984375;
    }
}