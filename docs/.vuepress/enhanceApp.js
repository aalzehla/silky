export default ({ router }) => {
    /**
     * Route switch event handling
     */
    router.beforeEach((to, from, next) => {
        console.log("switch route", to.fullPath, from.fullPath);

        //Trigger Baidu'spvstatistics
        if (typeof _hmt != "undefined") {
            if (to.path) {
                _hmt.push(["_trackPageview", to.fullPath]);
                console.log("Report to Baidu statistics", to.fullPath);
            }
        }

        // continue
        next();
    });
};