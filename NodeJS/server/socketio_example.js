// node js code
const express = require('express');
const app = express();
const http = require('http').createServer(app);
const bodyParser = require('body-parser')
const io = require("socket.io")(http);



let users = new Object();


// use static dirs (no templating engine)
app.use(express.static(__dirname + "/www"));

// parse application/json
app.use(bodyParser.json());




app.get('/users', (req, res) => {
  console.log("GET /users");
    res.sendfile(__dirname + '/www/users.html');
});
app.get('/users/list', (req, res) => {
  // console.log("GET /users/list >> req: " + req + ", res: " + res);

  _u = [];
  let counter = 0;
  for(var key in users) {
    _u[counter] = {
      id: key,
      name: users[key].name
    };
    counter++;
  }

  res.json({
    "result" : "success",
    "data" : _u
  });
});



class User {
  constructor(_name) {
    this.name = _name;
  }
}




io.on('connection', (socket) => {
  console.log(new Date().toTimeString().split(" ")[0] + ' >> a user connected >> socket.id' + socket.id);

  if (users[socket.id] != undefined) {
    console.log("this socket connection already exists (..?)");
  } else {
    users[socket.id] = new User();
    console.log("CREATE NEW USER >> users[" + socket.id + "]: " , users[socket.id]);
  }

  console.log("Object.keys(users).length: " + Object.keys(users).length + ", io.sockets.clients(): ", Object.keys(io.sockets.sockets).length);

  socket.on('chat message', (msg) => {
    io.emit('chat message', msg);
  });

  socket.on('fname', (msg) => {
    if (typeof msg === "object") { // if "object" then we assume that it's a JSON object
      if (msg.hasOwnProperty("name")) {
        msg = msg.name;
        console.log("socket.on('fname') >> msg is now " + msg);
      }
    }
    socket.emit("fnameReturn", {
      msg: "welcome " + msg
    });
    users[socket.id].name = msg;
    console.log("io >> fname >> got a new name: ", msg);
    console.log("io >> fname >> users[" + socket.id + "].name: " , users[socket.id].name);

    // could also consider this: sending to all clients except sender
    // socket.broadcast.emit('broadcast', 'hello friends!');

    io.emit('fnameNew', {
      msg: "got a new name",
      name: msg
    });

    for (var key in users) {
      // check if the property/key is defined in the object itself, not in parent
      if (users.hasOwnProperty(key)) {
        if (users[key].name !== undefined) {
          console.log("==>", users[key].name);
        }
      }
    }
  });
  socket.on('disconnect', () => {
    console.log('user disconnected >> removing user ' + users[socket.id].name);
    delete users[socket.id];
  });
});

// // Init Express - listen on target port
// app.listen(3000, function(err) {
//   if (err) {
//     return console.log('something bad happened', err)
//   }

//   console.log(`server is listening on 3000`);
// });

http.listen(3000, () => {
  console.log('Connected at 3000');
});