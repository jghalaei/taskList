import React from "react";
import { Form, FormGroup, Button, Row, Col, Modal } from "react-bootstrap";
import { propTypes } from "react-bootstrap/esm/Image";

const TaskForm = ({ task, mode, onSubmit, onCancel }) => {
  const [title, setTitle] = React.useState("");
  const [dueDate, setDueDate] = React.useState("");
  const [dueTime, setDueTime] = React.useState("");
  const [comment, setComment] = React.useState("");
  const [status, SetStatus] = React.useState("");

  React.useEffect(() => {
    if (task) {
      setTitle(task.title);

      const parsedDueDate = new Date(task.dueDate);
      const formattedDate = parsedDueDate.toLocaleDateString("en-CA");
      const formattedTime = parsedDueDate.toLocaleTimeString([], {
        hour12: false,
        hour: "2-digit",
        minute: "2-digit",
      });
      setDueDate(formattedDate);
      setDueTime(formattedTime);
      setComment(task.comment);
      SetStatus(task.status);
      
    }
  }, [task]);

  const onSubmitClicked = () => {
    const combinedDate = new Date(dueDate);
    combinedDate.setHours(dueTime.split(":")[0]);
    const returnTask = {
      id: task ? task.id : "",
      title: title,
      dueDate: combinedDate.toISOString(),
      comment: comment,
      status: status,
    };
    onSubmit(returnTask);

  };
  return (
    <Modal show={true}>
      <Modal.Header>
        <Modal.Title>
          {mode === "Add"
            ? "Add Task"
            : mode === "View"
            ? "View Task"
            : "Edit Task"}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <FormGroup>
            <Form.Label>Title</Form.Label>
            <Form.Control
              type="text"
              readOnly={mode === "View"}
              value={title}
              onChange={(e) => setTitle(e.target.value)}
            />
          </FormGroup>
          <FormGroup>
            <Form.Label>Due Date</Form.Label>
            <Form.Control
              type="date"
              readOnly={mode === "View"}
              value={dueDate}
              onChange={(e) => setDueDate(e.target.value)}
            />
          </FormGroup>
          <FormGroup>
            <Form.Label>Due Time</Form.Label>
            <Form.Control
              type="time"
              readOnly={mode === "View"}
              value={dueTime}
              onChange={(e) => setDueTime(e.target.value)}
            />
          </FormGroup>
          <FormGroup>
            <Form.Label>Comment</Form.Label>
            <Form.Control
              as="textarea"
              readOnly={mode === "View"}
              rows="3"
              value={comment}
              onChange={(e) => setComment(e.target.value)}
            />
          </FormGroup>
          <FormGroup>
            <Form.Label>Status</Form.Label>
            <Form.Select
              as="select"
              value={status}
              onChange={(e) => SetStatus(e.target.value)}
            >
              <option value="Created">Created</option>
              <option value="InProgress">InProgress</option>
              <option value="Completed">Completed</option>
              <option value="Canceled">Canceled</option>
            </Form.Select>
          </FormGroup>
          <Row className="mt-3 justify-content-right"></Row>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Col className="w-50"> </Col>
        {mode !== "View" && (
          <Button
            variant="primary"
            type="submit"
            className="me-3 mb-3 mx-auto w-25"
            onClick={onSubmitClicked}
          >
            Submit
          </Button>
        )}
        <Button
          variant="secondary"
          type="submit"
          className="me-3 mb-3 mx-auto w-25"
          onClick={onCancel}
        >
          {mode === "View" ? "Close" : "Cancel"}
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
TaskForm.propTypes = {
  task: propTypes.object,
  mode: propTypes.string,
  onSubmit: propTypes.func,
};
TaskForm.defaultProps = {
  task: { title: "", dueDate: "", comment: "", status: 0 },
  mode: "Add",
};
export default TaskForm;
