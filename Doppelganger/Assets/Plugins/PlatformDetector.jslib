mergeInto(LibraryManager.library, {
    GetBrowserPlatform: function () {
        var platform = navigator.platform || "unknown";
        var userAgent = navigator.userAgent || "unknown";
        // Send the result back to Unity (optional, or return value)
        console.log("Platform: " + platform);
        return allocate(intArrayFromString(platform), 'i8', ALLOC_STACK);
    }
});