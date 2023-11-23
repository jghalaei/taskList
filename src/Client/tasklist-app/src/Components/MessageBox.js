import React from "react";
import { Modal, Button } from "react-bootstrap";
const MessageBox = ({ title, message, DialogMode, onYes, onClose }) => {
  return (
    <Modal show={true}>
      <Modal.Header>
        <Modal.Title>{title}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <p>{message}</p>
      </Modal.Body>
      <Modal.Footer>
        {DialogMode === "Ok" && (
          <>
            <Button variant="primary" onClick={onYes}>
              OK
            </Button>
          </>
        )}

        {DialogMode === "YesNo" && (
          <>
            <Button variant="secondary" onClick={onClose}>
              No
            </Button>
            <Button variant="primary" onClick={onYes}>
              Yes
            </Button>
          </>
        )}
      </Modal.Footer>
    </Modal>
  );
};

export default MessageBox;
