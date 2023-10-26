if (Globalize && Globalize.load) {
    var likelySubtags = {
        supplemental: {
            likelySubtags: {
                en: "en-Latn-US",
                de: "de-Latn-DE",
                ru: "ru-Cyrl-RU",
                tr: "tr-Latn-TR"
            }
        }
    };
    if (!Globalize.locale()) {
        Globalize.load(likelySubtags);
        Globalize.locale("tr")
    }

}