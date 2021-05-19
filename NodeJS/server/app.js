///////////////////////////////////////////////////////////////////////////
//
//  libs
//
///////////////////////////////////////////////////////////////////////////
var config                = require('config.json')('config/settings.json');  // load config.json, allowing for external configging of this node app
var midi                  = require('midi');          // MIDI
const { Server, Client, Message } = require('node-osc/lib');
const fs                  = require('fs');
const express             = require('express');
const bodyParser          = require('body-parser')


///////////////////////////////////////////////////////////////////////////
//
//  default variable values
//
///////////////////////////////////////////////////////////////////////////
const expressPort = 3000;




///////////////////////////////////////////////////////////////////////////
//
//  Define instances that we'll be using
//
///////////////////////////////////////////////////////////////////////////
// define Express instance
const app = express();

///////////////////////////////////////////////////////////////////////////
//
//  Express web server stuff
//
///////////////////////////////////////////////////////////////////////////

// use static dirs (no templating engine)
app.use(express.static(__dirname + "/www"));

// parse application/json
app.use(bodyParser.json())

// GET
app.get('/mididevices', function(request, response) {

  let res = { "mididevices" : [] };
  let numPorts = input.getPortCount();
  for (let i = 0; i < numPorts; i++ ){
    if (!existingMidiDevices.includes(input.getPortName(i))) {
      res["mididevices"].push( input.getPortName(i) );
    }
  }
  res["selectedDevice"] = midiPrefDevice;

  response.json({
    "result" : "success",
    "data": res
  });
});
// POST
app.post('/mididevice', function(request, response) {
  console.log("SET >> /mididevice >> " , request.body);

  if (request.body.hasOwnProperty("devicename")) {
    if (midiPrefDevice === request.body.devicename) {
      console.log("the provided device is already the preferred device..");
    }
    midiPrefDevice = request.body.devicename;

    // save to file immediately
    saveMidiPref(midiPrefDevice);

    openRequiredMidiport(midiPrefDevice);
  }

  response.json({
    "result" : "success"
  });
});

// Init Express - listen on target port
app.listen(expressPort, function(err) {
  if (err) {
    return console.log('something bad happened', err)
  }

  console.log(`server is listening on ${expressPort}`);
});




var app = express();

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

// uncomment after placing your favicon in /public
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', routes);
app.use('/users', users);

// catch 404 and forward to error handler
app.use(function(req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
  app.use(function(err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', {
      message: err.message,
      error: err
    });
  });
}

// production error handler
// no stacktraces leaked to user
app.use(function(err, req, res, next) {
  res.status(err.status || 500);
  res.render('error', {
    message: err.message,
    error: {}
  });
});


module.exports = app;
