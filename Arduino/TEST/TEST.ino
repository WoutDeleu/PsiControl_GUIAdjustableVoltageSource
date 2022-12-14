
#define SIZE_ADDRESSPINS 8
#define SIZE_DATAPINS 8
#define SIZE_CARDPINS 4

#define INPUT_SELECTOR 0
#define OUTPUT_SELECTOR 1
// #include <CmdMessenger.h>

enum class Register
{
  // 00H
  MEASURE = 0,
  // 01H
  BUSCON0 = 1,
  // 02H
  BUSCON1 = 2,
  // 03H
  GNDCON0 = 3,
  // 04H
  GNDCON1 = 4,
  // 05H
  SOURCE = 5,
  // 06H
  DACDATA0 = 6,
  // 07H
  DACDATA1 = 7,
  // 08H
  ERROR_FLAGS = 8,
  // 09H
  RANGE = 9,
  // 0AH
  WAVE_CTRL = 10,
  // 0BH
  WAVE_AMPL0 = 11,
  // 0CH
  WAVE_AMPL1 = 12,
  // 0DH
  WAVE_FREQ0 = 13,
  // 0EH
  WAVE_FREQ1 = 14,
  // 0FH
  WAVE_DUTY = 15,
  // FAH
  READ_SOURCE = 250,
  // FBH
  READ_BUSCON0 = 251,
  // FCH
  READ_BUSCON1 = 252,
  // FDH
  READ_GNDCON0 = 253,
  // FE
  READ_GNDCON1 = 254,
  // FFH
  IDENT = 255,
};
enum class MeasRange
{
  /// Do not strip down.
  Bi10 = 1,
  /// Strip down with a divider factor of 3
  Bi30 = 3,
  /// Strip down with a divider factor of 12
  Bi120 = 12,
};

// BoardNr
// Must be able to be changed in GUI
int boardNumber = 0x00;

// Data pins
// const int d0_pin = 2;
// const int d1_pin = 3;
// const int d2_pin = 4;
// const int d3_pin = 5;
// const int d4_pin = 6;
// const int d5_pin = 7;
// const int d6_pin = 8;
// const int d7_pin = 9;
const int datapins[SIZE_DATAPINS] = {2, 3, 4, 5, 6, 7, 8, 9};

// Address pins
// const int a0_pin = 18;
// const int a1_pin = 19;
// const int a2_pin = 20;
// const int a3_pin = 21;
// const int a4_pin = 22;
// const int a5_pin = 23;
// const int a6_pin = 24;
// const int a7_pin = 25;
const int addresspins[SIZE_ADDRESSPINS] = {18, 19, 20, 21, 22, 23, 24, 25};

// Card address pins
// const int a8_pin = 37;
// const int a9_pin = 38;
// const int a10_pin = 39;
// const int a11_pin = 40;
const int cardAddresspins[SIZE_CARDPINS] = {37, 38, 39, 40};

// Controller pins
// Remark: active low
const int WR = 51;
const int RD = 52;
const int RESET = 53;

// Pull_up pins
const int ACK = 28;
const int ERR = 29;

const byte AD0 = A14;
const byte AD1 = A13;

/// The maximum number of acknowledge check retries.
const int MAX_ACK_CHECK_RETRIES = 100;
/// The number of AIO channels for one board.
const int AIO_CHANNELS = 16;
/// Time-out to switch relay on.
static int RELAY_ON_SETTLING = 5;
/// Time-out to switch relay off.
static int RELAY_OFF_SETTLING = 1;
/// The input impedance of the measure circuit. (1M2)
const double MEAS_INPUT_IMP = 1200000;

/// 2 registers - each 1 byte - in total 2 bytes
/// The data 0 status register, needed for u an i source
int dacData0Status;
/// The data 1 status register, needed for u an i source
int dacData1Status;

/// The source status register
int sourceStatus;

/// 2 registers - each 1 byte - in total 2 bytes
/// The bus connection 0 status register
int busCon0Status;
/// The bus connection 1 status register
int busCon1Status;

/// 2 registers - each 1 byte - in total 2 bytes
/// The ground connection 0 status register.
int gndCon0Status;
/// The ground connection 1 status register.
int gndCon1Status;

/// The measure status register.
int measureStatus;
/// The U/I bus status register.
int rangeStatus = 0;

void setupStatus()
{
  dacData0Status = 0x00;
  dacData1Status = 0x80;
  sourceStatus = 0x00;
  busCon0Status = 0x00;
  busCon1Status = 0x00;
  gndCon0Status = 0x00;
  gndCon1Status = 0x00;
  measureStatus = 0x00;
  rangeStatus = 0x00;
  // The DAC is reset
  writeData(Register::DACDATA0, dacData0Status, boardNumber);
  writeData(Register::DACDATA1, dacData1Status, boardNumber);
  // The SOURCE register is reset
  writeData(Register::SOURCE, sourceStatus, boardNumber);
  // Rhe MEASURE register is reset
  writeData(Register::MEASURE, measureStatus, boardNumber);
  // All relays are switched off
  writeData(Register::BUSCON0, busCon0Status, boardNumber);
  writeData(Register::BUSCON1, busCon1Status, boardNumber);
  writeData(Register::GNDCON0, gndCon0Status, boardNumber);
  writeData(Register::GNDCON1, gndCon1Status, boardNumber);
  // The UI-bus register is reset.
  writeData(Register::RANGE, rangeStatus, boardNumber);
  // Read the errorflags to clear the register
  readData(Register::ERROR_FLAGS, boardNumber);
  // settling time
  delay(RELAY_OFF_SETTLING);
}

// // Startup cmdMessenger and link to serial port
// CmdMessenger cmdMessenger = CmdMessenger(Serial);
// // List all the commands
// enum
// {
//   kSetLed, // Command to request led to be set in specific state
// };
// Callbacks define on which received commands we take action
// Link function to enum
// void attachCommandCallbacks()
// {
//   cmdMessenger.attach(kSetLed, OnSetLed);
// }
bool ledState = 0;
// Callback function that sets led on or off
// void OnSetLed()
// {
//   // Read led state argument, interpret string as boolean
//   ledState = cmdMessenger.readBoolArg();
//   // Set led
//   digitalWrite(14, ledState ? HIGH : LOW);
// }

void setup()
{
  Serial.begin(115200);
  setupPins();
  setupStatus();

  // Adds newline to every command
  // cmdMessenger.printLfCr();

  // Attach my application's user-defined callback methods
  // attachCommandCallbacks();
}
void loop()
{
  // digitalWrite(14, HIGH);
  // connectToBus(1, true);
  // connectVoltageSource(true);
  // setVoltage(11);
  // Serial.println("***********");
  // double current = measureCurrentUsource();
  // Serial.println("Measured current = " + String(current));
  // current = measureVoltage(1);
  // Serial.println("Measured Voltage = " + String(current));
  // Serial.println("***********");
  // Serial.println();
  // delay(5000);
  // digitalWrite(14, LOW);
  // setVoltage(0);
  // Serial.println("***********");
  // current = measureCurrentUsource();
  // Serial.println("Measured current = " + String(current));
  // current = measureVoltage(1);
  // Serial.println("Measured Voltage = " + String(current));
  // Serial.println("***********");
  // Serial.println();
  // delay(5000);

  digitalWrite(14, LOW);

  // String t = cmdMessenger.readStringArg();
  // Serial.print("string is " + t);
}
