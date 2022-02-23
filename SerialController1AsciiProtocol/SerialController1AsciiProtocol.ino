#define SENSOR 33 // 33 controller 1, 0 controller 2
#define ACTUATOR 25 // 25 controller, 23 controller 2

void setup()
{
  Serial.begin(115200);
  pinMode(SENSOR, INPUT_PULLUP);
  pinMode(ACTUATOR, OUTPUT);
}

void loop()
{

  if (Serial.available() > 0) {

      // IMPLEMENTA EL PROTOCOLO

  }
}
