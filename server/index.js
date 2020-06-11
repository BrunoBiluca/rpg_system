var express = require('express')

var app = express()
app.use(express.static('public'))

app.get("/", (request, response) => {
    response.send("Final Fantasy 7 REMAKE")
})

app.listen(3000, function () {
    console.log('Example app listening on port 3000!');
});