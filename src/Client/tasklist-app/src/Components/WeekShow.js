import React, { useEffect } from "react";
import { Container, Row, Col, Button } from "react-bootstrap";
import DayShow from "./DayShow";
const WeekShow = ({ showDate, tasks, onEdit, onDelete, setShowDate }) => {
  const getFirstDayOfWeek = (date) => {
    const dat = new Date(date);
    const dayOfWeek = dat.getDay();
    const daysUntilFirstDay = dayOfWeek === 0 ? 6 : dayOfWeek - 1;
    const firstDayOfWeek = new Date(dat);
    firstDayOfWeek.setDate(dat.getDate() - daysUntilFirstDay);
    return firstDayOfWeek;
  };

  const startDate = getFirstDayOfWeek(showDate);
  const daysOfWeek = [...Array(7)].map((_, index) => {
    const day = new Date(startDate);
    day.setDate(day.getDate() + index);
    return day;
  });

  return (
    <Container>
      <Row className="bg-primary rounded mb-3 text-white text-center ">
        <Col className="col-1 text-center">
          <Button
            onClick={() => setShowDate(showDate - 7 * 24 * 60 * 60 * 1000)}
          >
            {"<<"}
          </Button>
        </Col>

        <Col>
          <p>
            {startDate.toLocaleDateString("en-US")} to{" "}
            {daysOfWeek[6].toLocaleDateString("en-US")}
          </p>
        </Col>
        <Col className="col-1 text-center">
          <Button
            onClick={() => setShowDate(showDate + 7 * 24 * 60 * 60 * 1000)}
          >
            {">>"}
          </Button>
        </Col>
      </Row>
      <Row className="rounded">
        {daysOfWeek.map((day, index) => (
          <Col key={index} className="col mb-3 p-3">
            <Row className="border bg-light rounded">
              <div>{day.toLocaleDateString("en-US", { weekday: "short" })}</div>
              <div>{day.getDate()}</div>
            </Row>
            <DayShow
              key={day}
              tasks={tasks.filter((task) => {
                const dueDate = new Date(task.dueDate);
                return dueDate.toDateString() === day.toDateString();
              })}
              onEdit={onEdit}
              onDelete={onDelete}
            />
          </Col>
        ))}
      </Row>
    </Container>
  );
};

export default WeekShow;
