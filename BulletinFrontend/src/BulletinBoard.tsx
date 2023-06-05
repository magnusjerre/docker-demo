import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import "./BulletinBoard.css";
import SubmitBulletinBoardMessageForm from "./SubmitBulletinBoardMessageForm";

export interface IBulletinBoardMessage {
    id: number;
    posterId: number;
    posterName: string;
    message: string;
}

const BulletinBoard = () => {
    const { isAuthenticated } = useAuth0();
    const [messages, setMessages] = useState([]);

    const fetchMessages = () => {
        const url = import.meta.env.VITE_API_URL + "/bulletinboard";
        // console.log("url to fetch from", url);
        fetch(url, {
            mode: 'cors',
            headers: {
                "accept": "text/plain"
            }
        })
        .then(resp => resp.json())
        .then(json => {
            setMessages((json as []).reverse());
        });
    }

    useEffect(() => {
            fetchMessages();
    }, [0]);

    return (
    <div className="BulletinBoard">
        {isAuthenticated && (
        <SubmitBulletinBoardMessageForm onAddMessage={(m: IBulletinBoardMessage) => {
            const newMessages = [m, ...messages] as any;
            setMessages(newMessages);
        }}></SubmitBulletinBoardMessageForm>)}
    <h2>Meldinger</h2>
    <ul className="bulletinboard">
        {
            messages.map((bbm: IBulletinBoardMessage) => (<li key={bbm.id}>
                <p>{bbm.message} - {bbm.posterName}</p>
            </li>))
        }
    </ul>
    </div>);
}

export default BulletinBoard;