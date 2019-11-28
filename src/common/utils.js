export const notify = function (obj, title, content) {
  const h = obj.$createElement;

  obj.$notify({
    title: title,
    message: h("i", {
      style: "color: teal"
    }, content)
  });
}
