import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import "./BulletinBoard.css";
import SubmitBulletinBoardMessageForm from "./SubmitBulletinBoardMessageForm";

export interface IBulletinBoardMessage {
    id: number;
    posterId: string;
    message: string;
}

const BulletinBoard = () => {
    const { isAuthenticated } = useAuth0();
    const [messages, setMessages] = useState<IBulletinBoardMessage[]>([]);
    const [fetchingMessages, setFetchingMessages] = useState(false);

    const fetchMessages = () => {
        setFetchingMessages(true);
        const url = import.meta.env.VITE_API_URL + "/bulletinboard";
        fetch(url, {
            mode: 'cors',
            headers: {
                "accept": "text/plain"
            }
        })
            .then(resp => resp.json())
            .then(json => {
                setMessages((json as []).reverse() as IBulletinBoardMessage[]);
                setFetchingMessages(false);
            });
    }

    const onAddMessageHandler = (m: IBulletinBoardMessage) => {
        setMessages([m, ...messages] as IBulletinBoardMessage[]);
    };

    useEffect(() => {
        fetchMessages();
    }, [0]);

    return (
        <div className="bulletinboard-wrapper">
            {isAuthenticated && (
                <SubmitBulletinBoardMessageForm onAddMessage={onAddMessageHandler}></SubmitBulletinBoardMessageForm>)}
            <h2>Meldinger</h2>
            <ul className="bulletinboard">
                {
                    messages.map((bbm: IBulletinBoardMessage) => (<li key={bbm.id}>
                        <p>{bbm.message}</p>
                    </li>))
                }
            </ul>
        </div>);
}

export default BulletinBoard;