/**
 * Created by adriaanwormgoor on 14/05/2021.
 */


let $scr1, $scr2, $fname, fname = null;
let screens = [];
let socket = io();

socket.on("connect", () => {
  console.log("socketio >> f:connect >> socket id: " + socket.id);
  if (fname != null) {
    sendFname(fname);
  }
});

socket.on('fnameReturn', onFnameReturn);
socket.on('fnameNew', onFnameNew);

function onFnameReturn(data) {
  console.log("Socket.io >> f:onFnameReturn >> data: ", data);
}

function onFnameNew(data) {
  console.log("Socket.io >> f:onFnameNew >> data: ", data);
}

function checkNewMidiDevices() {
  //console.log("f:checkNewMidiDevices");

  getAvailableMidiDevices(function(data) {
    let len = $dropdown.children("option").length;

    if (len !== (data.mididevices.length + 1)) {
      // new number of items >> repopulating
      console.log("f:checkNewMidiDevices >> new number of items >> repopulating");
      populateDropdown(data.mididevices);

      if (data.selectedDevice.length > 0 && data.mididevices.includes(data.selectedDevice)) {
        $dropdown.val(data.selectedDevice);
      }
    } else {
      //console.log("f:checkNewMidiDevices >> same number of items >> doing nothing");
      // same number of items >> doing nothing
    }
  });
}

// empties the dropdown list
function emptyDropdown(callback) {
  $dropdown.find("option").each(function(index, value) {
    $(this).remove();
  });

  if (typeof callback === "function") {
    callback();
  }
}

// populates the dropdown list with the provided list of devices (insert <none> entry at top)
function populateDropdown(devices) {
  console.log("f:populateDropdown >> devices: " , devices);

  emptyDropdown();

  $dropdown.append('<option value="<none>">none</option>');
  devices.forEach(function(el) {
    //console.log(el);
    $dropdown.append('<option value="' + el + '">' + el + '</option>');
  });
}

// GET - get list of available midi devices from server
function getAvailableMidiDevices(callback) {

  $.ajax({
    url: "/mididevices",
    type: "GET",
    success: function(res) {
      //console.log("got response from server >> data: ", res.data);
      if (res.result === "success" ) {
        if (typeof callback === "function") {
          callback(res.data);
        }
      } else {
        console.warn("something went wrong getting the list of Midi devices names");
      }
    },
    dataType: "json"
  });
}

// SET
function setMidiDeviceTo(device) {
  console.log("f:setMidiDeviceTo >> telling server to select midi device '" + device + "'");

  let json = {
    "devicename" : device
  };

  console.log("\t json: " , json);
  $.ajax({
    url: "/mididevice",
    type: "POST",
    data: JSON.stringify(json),
    contentType: "application/json",
    success: function(res) {
      //console.log("got response from server >> data: ", res.data);
      if (res.result === "success" ) {
        if (typeof callback === "function") {
          callback(res.data);
        }
      } else {
        console.warn("something went wrong getting the list of Midi devices names");
      }
    },
    dataType: "json"
  });

}


function sendFname(name) {
  socket.emit("fname", { "name" : name });
  // socket.emit("fname", name);
}

function initScreen1() {
  $("#fnameSubmit").on("click tap tapend", function(e) {

    console.log("field value: " + $fname[0].value);
    fname = $fname[0].value;
    sendFname(fname);

    // // only submit if the selected item is not the first item (ie: '<none>')
    // if ($dropdown.find("option:selected")[0] !== $dropdown.children("option")[0]) {
    //   setMidiDeviceTo($dropdown.find("option:selected")[0].value);
    // }

    e.preventDefault();
    e.stopPropagation();
  });
}

function showScreen(scr) {
  $.each(screens, function(i, val) {
    val.hide();
  });
  scr.show();
}
$(document).ready(function() {
  console.log("ready");

  $fname = $("#fname");

  $scr1 = $("#scr1");
  $scr2 = $("#scr2");

  screens[0] = $scr1;
  screens[1] = $scr2;

  showScreen($scr1);


  $container = $("#container");

  initScreen1();
  // $("#selectMidiDevice").on("click tap tapend", function(e) {

  //   // only submit if the selected item is not the first item (ie: '<none>')
  //   if ($dropdown.find("option:selected")[0] !== $dropdown.children("option")[0]) {
  //     setMidiDeviceTo($dropdown.find("option:selected")[0].value);
  //   }

  //   e.preventDefault();
  //   e.stopPropagation();
  // });

  // $dropdown = $("#mididevices_dropdown");
  // $dropdown.change(function(e) {
  //   console.log("dropdown selection changed to ", $(this).find("option:selected")[0].value);
  // });

});
