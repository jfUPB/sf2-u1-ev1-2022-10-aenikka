#define SENSOR1 0
#define SENSOR2 4
#define SENSOR3 16
#define ACTUATOR1 26
#define ACTUATOR2 25
#define ACTUATOR3 32


void setup()
{
  Serial.begin(115200);
  pinMode(SENSOR1, INPUT_PULLUP);
  pinMode(SENSOR2, INPUT_PULLUP);
  pinMode(SENSOR3, INPUT_PULLUP);
  pinMode(ACTUATOR1, OUTPUT);
  pinMode(ACTUATOR2, OUTPUT);
  pinMode(ACTUATOR3, OUTPUT);
}

void loop()
{
  uint8_t dataFrame[3];

  if (Serial.available() > 2) {

    dataFrame[0] = Serial.read();
    dataFrame[1] = Serial.read();
    dataFrame[2] = Serial.read();

    if (dataFrame[0] == 0x01) {
      Serial.write(0x03);
      Serial.write(digitalRead(SENSOR1));
      Serial.write(digitalRead(SENSOR2));
      Serial.write(digitalRead(SENSOR3));
    } else if (dataFrame[0] == 0x02) {
      Serial.write(0x03);
      Serial.write(digitalRead(ACTUATOR1));
      Serial.write(digitalRead(ACTUATOR2));
      Serial.write(digitalRead(ACTUATOR3));
    }
    else if (dataFrame[0] == 0x03) {
      if (dataFrame[1] == 0x01) {
        digitalWrite(ACTUATOR1, dataFrame[2]);
      } else if (dataFrame[1] == 0x02) {
        digitalWrite(ACTUATOR2, dataFrame[2]);
      } else if (dataFrame[1] == 0x03) {
        digitalWrite(ACTUATOR3, dataFrame[2]);
      }
    }

  }
}
